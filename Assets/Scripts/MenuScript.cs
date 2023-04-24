using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]  private Button play;
    [SerializeField]  private Button quit;
    [SerializeField]  private Button scores;

    private void Start()
    {
        play.onClick.AddListener(PlayGame);
        quit.onClick.AddListener(QuitGame);
        scores.onClick.AddListener(Scores);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Scores()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
