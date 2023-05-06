using Supercyan.AnimalPeopleSample;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    private Text gameOver, buttonText, speedText, exitText, jumpBoost, pointsBoost;
    private Button restart, exit;
    private SimpleSampleCharacterControl sCP = null;
    private PointsSave poinstSave;
    private ScoreManager scoreManager;
    private Timer timer;
    private TimerPoints timerPoints;
    void Start()
    {
        sCP = GameObject.FindObjectOfType<SimpleSampleCharacterControl>();

        gameOver = GameObject.FindWithTag("GameOver").GetComponent<Text>();

        speedText = GameObject.FindWithTag("Speed").GetComponent<Text>();

        restart = GameObject.FindWithTag("Restart").GetComponent<Button>();
        buttonText = GameObject.FindWithTag("RestartText").GetComponent<Text>();

        exit = GameObject.FindWithTag("Exit").GetComponent<Button>();
        exitText = GameObject.FindWithTag("ExitText").GetComponent<Text>();

        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        timer = GameObject.FindObjectOfType<Timer>();
        timerPoints = GameObject.FindObjectOfType<TimerPoints>();

        jumpBoost = GameObject.FindWithTag("JumpBoost").GetComponent<Text>();
        pointsBoost = GameObject.FindWithTag("PointsBoost").GetComponent<Text>();

        restart.onClick.AddListener(Restar);
        exit.onClick.AddListener(Exit);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sCP.AudioStop();
            timer.AudioStop();
            timerPoints.AudioStop();

            gameOver.enabled = true;

            jumpBoost.enabled = false;
            pointsBoost.enabled = false;  

            restart.enabled = true;
            restart.image.enabled = true;
            buttonText.enabled = true;

            exit.enabled = true;
            exit.image.enabled = true;
            exitText.enabled = true;

            sCP.SetSpeed(0);
            sCP.SetIdle();
        }

    }

    public void Exit()
    {
       SaveData();
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    public void Restar()
    {
        SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void SaveData()
    {
        poinstSave = GameObject.FindObjectOfType<PointsSave>();
        if (poinstSave.IsSave() == false)
        {
            poinstSave.SetIsSave(true);
            poinstSave = GameObject.FindObjectOfType<PointsSave>();
            scoreManager.AddScore(new Score(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), poinstSave.GetPoints()));
        }
    }
}
