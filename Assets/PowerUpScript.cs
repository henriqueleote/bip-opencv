using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private PointsSave points;
    private TimerPoints tp;
    void Start()
    {
        points = GameObject.FindObjectOfType<PointsSave>();
        tp = GameObject.FindObjectOfType<TimerPoints>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            points.SetModified(true);
            points.SetPoints(points.GetPoints());
            tp.SetTimer();
        }
    }
}
