using UnityEngine.UI;
using UnityEngine;

public class TimerPoints : MonoBehaviour
{
    public bool timer = false;
    public float timerleft = 0;
    private PointsSave points;
    private new AudioSource audio;
    private bool isPlaying;
    private Timer timerG;

    public void Start()
    {
        points = GameObject.FindObjectOfType<PointsSave>();
        audio = GetComponent<AudioSource>();
        timerG = GameObject.FindObjectOfType<Timer>();
        audio.time = 1f;
        isPlaying = false;
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
                points.SetModified(false);  
                audio.Stop();
                isPlaying = false;
            }
        }

    }

    public void SetTimer()
    {
        timer = true;
        timerleft = 10;
        if (timerG.IsPlaying())
        {
            timerG.AudioStop();
            timerG.SetNotPlaying();
        }
        audio.Play();
        isPlaying = true;
    }

    public void AudioStop()
    {
        audio.Stop();
    }

    public bool IsPlayingPoints()
    {
        return isPlaying;
    }

    public void SetNotPlaying()
    {
        isPlaying = false;
    }
}
