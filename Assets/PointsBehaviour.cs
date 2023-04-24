using Supercyan.AnimalPeopleSample;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PointsBehaviour : MonoBehaviour
{
    private Text pointsText,speedText;
    private float points = 0f;
    private float speed = 0;
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

        if (poinstSave.GetModified())
        {
            poinstSave.TriplePoints();
        }

        points = poinstSave.GetPoints();

        speed = sCP.GetSpeed() * 1.2f;

        if (speed == 0) return;

        sCP.SetSpeed(speed);

        points += 1 + GetSpeedFromPoints();


        poinstSave.SetPoints(points);


        pointsText.text = "Points: " + Math.Round(points, 2).ToString();
        speedText.text = "Speed: " + Math.Round(speed, 2).ToString();
    }

    float GetSpeedFromPoints()
    {
        return (float)speed * 1.5f;
    }

}
