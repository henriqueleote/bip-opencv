using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supercyan.AnimalPeopleSample
{
    public class SimpleSampleCharacterControl : MonoBehaviour
    {

        private float desiredX = 0;
        private float m_moveSpeed = 4f;
        private float m_jumpForce = 7f;
        private float fallMultiplier = 2f;

        private new AudioSource audio;

        [SerializeField] private Animator m_animator = null;
        [SerializeField] private Rigidbody m_rigidBody = null; 

        private bool m_wasGrounded;

        private float m_jumpTimeStamp = 0;
        private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded;
        private bool m_isIdle;
        private bool m_canMove = true;

        private List<Collider> m_collisions = new List<Collider>();

        public void AudioStop()
        {
            audio.Stop();
        }

        public float GetSpeed()
        {
            return m_moveSpeed;
        }
        public void SetSpeed(float speed)
        {
            m_moveSpeed = speed;
        }

        public float GetJump()
        {
            return m_jumpForce;
        }
        public void SetJump(float jump)
        {
            m_jumpForce = jump;
        }

        public void SetIdle()
        {
            m_rigidBody.AddForce(0, 0, -m_moveSpeed);
            m_animator.SetFloat("MoveSpeed", 0);
            m_isIdle = true;
        }

        public void RemovetIdle()
        {
            m_isIdle = false;
            DirectUpdate();
        }


        private void Awake()
        {
            if (!m_animator) { gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { gameObject.GetComponent<Rigidbody>(); }
            audio = gameObject.GetComponent<AudioSource>();
            audio.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    m_isGrounded = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true; break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }

        private void Update()
        {
            if (!m_jumpInput && !m_isIdle)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    m_jumpInput = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    StartCoroutine(MoveRight());
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    StartCoroutine(MoveLeft());
                }
            }

            if (m_rigidBody.velocity.y < 0)
            {
                m_rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

        private IEnumerator MoveLeft()
        {
            if (transform.position.x > -0.5f && m_canMove)
            {
                m_canMove = false;
                desiredX -= 4.0f;
                yield return new WaitForSeconds(0.2f);
                m_canMove = true;
            }

            yield return null;
        }

        private IEnumerator MoveRight()
        {
            if (transform.position.x < 0.5f && m_canMove)
            {
                m_canMove = false;
                desiredX += 4.0f;
                yield return new WaitForSeconds(0.2f);
                m_canMove = true;
            }
            yield return null;
        }

        private void FixedUpdate()
        {
            m_animator.SetBool("Grounded", m_isGrounded);

            DirectUpdate();

            m_wasGrounded = m_isGrounded;
            m_jumpInput = false;
        }

        private void DirectUpdate()
        {
            if (!m_isIdle)
            {
                Vector3 forwardMove = transform.forward * m_moveSpeed * Time.fixedDeltaTime;

                Debug.Log(desiredX + " " + transform.position.x);
               
                if (desiredX > transform.position.x + 0.01f)
                {
                    forwardMove -= transform.right * Time.deltaTime * 10;
                }
                else if (desiredX < transform.position.x - 0.01)
                {
                    forwardMove += transform.right * Time.deltaTime * 10;
                }

                m_rigidBody.MovePosition(m_rigidBody.position + forwardMove);

                m_animator.SetFloat("MoveSpeed", m_moveSpeed);

                JumpingAndLanding();
            }
            
        }

        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            }

            if (!m_wasGrounded && m_isGrounded)
            {
                m_animator.SetTrigger("Land");
            }

            if (!m_isGrounded && m_wasGrounded)
            {
                m_animator.SetTrigger("Jump");
            }
        }
    }
}