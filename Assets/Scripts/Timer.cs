using Supercyan.AnimalPeopleSample;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool timer = false;
    public float timerleft = 0;
    private SimpleSampleCharacterControl sCP;
    private Text text;
    private new AudioSource audio;
    private bool isPlaying;
    private TimerPoints timerPoints;

    public void Start()
    {
        sCP = GameObject.FindObjectOfType<SimpleSampleCharacterControl>();
        text = GameObject.FindWithTag("JumpBoost").GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        audio.time = 1f;
        isPlaying = false;
        timerPoints = GameObject.FindObjectOfType<TimerPoints>();
    }

    void Update()
    {
        if (timer == true)
        {
            if (timerleft > 0)
            {
                timerleft -= Time.deltaTime;
            }
            else
            {
                timerleft = 0;
                timer = false;
                sCP.SetJump(7);
                text.enabled = false;
                audio.Stop();
                isPlaying = false;
            }
        }
        
    }

    public void SetTimer()
    {
        timer = true;
        timerleft = 10;
        text.enabled = true;
        text.text = "Super Jump!";
        if (timerPoints.IsPlayingPoints()) {
            timerPoints.AudioStop();
            timerPoints.SetNotPlaying();
        }
        audio.Play();
        isPlaying = true;
    }

    public void AudioStop()
    {
        audio.Stop();
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void SetNotPlaying()
    {
        isPlaying = false;
    }
}
