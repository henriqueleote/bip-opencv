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
            int r = Random.Range(0, 3);
            RenderSettings.skybox = skyboxes[r];
        }
    }
}
