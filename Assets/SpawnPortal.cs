using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public GameObject PortalPrefab;
    public GameObject PortalPrefab2;

    float timer = 0f;
    float duration = 15f; // the duration of the timer in seconds
    float z = -150f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Spawn();
            z -= 150f;
            timer = 0f;
        }
    }
    
    void Spawn()
    {
        int randomNumber = Random.Range(0, 2);
        
        if (randomNumber == 0)
        {
            Vector3 spawnPoint = new Vector3(0f, 1.5f, z); // replace this with the desired spawn location
            Instantiate(PortalPrefab, spawnPoint, Quaternion.Euler(0, 90, 0));
        }
        else
        {
            Vector3 spawnPoint = new Vector3(1.54f, 0.6f, z); // replace this with the desired spawn location
            Instantiate(PortalPrefab2, spawnPoint, Quaternion.Euler(0, 0, 0));
        }

    }
}
