using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShow_AND_WinAndOver : MonoBehaviour
{
    public static int maxScore;
    public Text TextScore;
    //public WhenNewRecord whenNewRecord;
    public GameObject WinFireworks;
    public GameObject GameOverMessege;
    bool ForCongratulationsWithNewRecord = true;

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
            if (ForCongratulationsWithNewRecord)
                StartCoroutine(ShowWinFireworks());
            //whenNewRecord.ShowWinFireworks();
        }
        TextScore.text = Score.score.ToString();
        if (Score.countApples == 20)
            StartCoroutine(ShowGameOverMessege());
    }
    IEnumerator ShowWinFireworks()
    {
        WinFireworks.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        WinFireworks.SetActive(false);
        ForCongratulationsWithNewRecord = false;
    }

    public IEnumerator ShowGameOverMessege()
    {
        GameOverMessege.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        GameOverMessege.SetActive(false);
        ChangingScenes.ExitGame();
    }
}
