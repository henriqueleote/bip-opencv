using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChangeSkyBox : MonoBehaviour
{
    public Material skybox;
    public Material skybox2;
    public Material skybox3;
    List<Material> skyboxes;

    void Start()
    {
        skyboxes = new List<Material> { skybox, skybox2, skybox3 };
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Create a list of indices that don't correspond to the current skybox
            List<int> validIndices = new List<int>();
            for (int i = 0; i < skyboxes.Count; i++)
            {
                if (skyboxes[i] != RenderSettings.skybox)
                {
                    validIndices.Add(i);
                }
            }

            // If there are valid indices, select a random one and set the skybox
            if (validIndices.Count > 0)
            {
                int randomIndex = validIndices[Random.Range(0, validIndices.Count)];
                RenderSettings.skybox = skyboxes[randomIndex];
            }
            
            Destroy(gameObject);
        }
    }
}
