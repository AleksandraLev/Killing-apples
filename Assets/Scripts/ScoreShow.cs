using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow : MonoBehaviour
{
    public static int maxScore;
    public string DataBaseName;
    public Text TextScore;

    void Start()
    {
        TextScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (maxScore < Score.score)
        {
            maxScore = Score.score;
        }
        TextScore.text = Score.score.ToString();
    }
}
