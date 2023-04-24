using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class ScoresScript : MonoBehaviour
{
    [SerializeField] private Button back;
    public RowUi rowUi;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        var scores = scoreManager.GetHighScores().ToArray();
        for (int i = 0; i < scores.Length; i++)
        {
            var row = Instantiate(rowUi, transform).GetComponent<RowUi>();
            if(row != null)
            {
                row.rank.text = (i + 1).ToString();
                row.score.text = Math.Round(scores[i].score,2).ToString();
                row.date.text = scores[i].date.ToString();
            }
            

        }

        back.onClick.AddListener(Back);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }



}
