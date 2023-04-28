using Supercyan.AnimalPeopleSample;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PointsBehaviour : MonoBehaviour
{
    private Text pointsText,speedText;
    private float points;
    public float k = 0.5f;
    public float baseSpeed;
    private SimpleSampleCharacterControl sCP = null;
    private PointsSave poinstSave;

    void Start()
    {
        pointsText = GameObject.FindWithTag("Points").GetComponent<Text>();
        speedText = GameObject.FindWithTag("Speed").GetComponent<Text>();
        sCP = GameObject.FindObjectOfType<SimpleSampleCharacterControl>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddPoint();
        }
    }

    void AddPoint()
    {
        poinstSave = GameObject.FindObjectOfType<PointsSave>();
        points = poinstSave.GetPoints();
        baseSpeed = sCP.GetSpeed();

        if (poinstSave.GetModified())
        {
            poinstSave.TriplePoints();
        }
        
        GetScoreFromSpeed(baseSpeed);
        UpdateSpeed();

        sCP.SetSpeed(baseSpeed);
        poinstSave.SetPoints(points);

        pointsText.text = "Points: " + Math.Round(points).ToString();
        speedText.text = "Speed: " + Math.Round(baseSpeed).ToString();
    }

    void UpdateSpeed()
    {
        baseSpeed *= Mathf.Exp(2 * k);
    }

    void GetScoreFromSpeed(float speed)
    {
        // Define the parameters for the exponential function
        float a = 10f;  // Controls the rate of increase
        float b = 0.2f;  // Controls the offset

        // Calculate the score using an exponential function
        points += a * Mathf.Exp(b * speed);
        
    }
}
