using Supercyan.AnimalPeopleSample;
using UnityEngine;

public class RocketUpScript : MonoBehaviour
{
    private SimpleSampleCharacterControl sCP;
    private Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        sCP = GameObject.FindObjectOfType<SimpleSampleCharacterControl>();
        timer = GameObject.FindObjectOfType<Timer>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sCP.SetJump(13);
            timer.SetTimer();
        }
    }
}
