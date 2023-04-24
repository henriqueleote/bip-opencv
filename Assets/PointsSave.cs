using UnityEngine;
using UnityEngine.UI;

public class PointsSave : MonoBehaviour
{
    private float points;
    private bool modified;
    private Text text;
    private bool isSave;

    public void Start()
    {
        modified = false;
        isSave = false;
        points = 0;
        text = GameObject.FindWithTag("PointsBoost").GetComponent<Text>();
    }

    public void SetIsSave(bool saved) { isSave = saved; }

    public bool IsSave() { return isSave; }

    public void SetPoints(float p) {
            points = p; }

    public float GetPoints() { return points; }

    public void SetModified(bool var) {
        modified = var;
        if (modified)
        {
            text.enabled = true;
            text.text = "Triple Points!";
        }
        else
        {
            text.enabled = false;
        }
    }
    public bool GetModified() { return modified; }

    public void TriplePoints()
    {
        points *= 3;
    }
}
