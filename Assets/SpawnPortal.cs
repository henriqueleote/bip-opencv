using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public GameObject PortalPrefab;

    float timer = 0f;
    float duration = 10f; // the duration of the timer in seconds
    float z = -200f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Spawn();
            z -= 200f;
            timer = 0f;
        }
    }
    
    void Spawn()
    {
        Vector3 spawnPoint = new Vector3(0f, -0.08430409f, z); // replace this with the desired spawn location
        Instantiate(PortalPrefab, spawnPoint, Quaternion.identity);
    }
}
