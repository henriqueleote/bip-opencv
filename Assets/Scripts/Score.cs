using System;

[Serializable]
public class Score
{
    public string date;
    public double score;

    public Score(string date, float score)
    {
        this.date = date;
        this.score = score;
    }

}
