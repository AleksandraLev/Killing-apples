using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ChangingScenes : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        //SceneManager.UnloadScene(1);
        //SceneManager.LoadScene(SceneManager.GetSceneByName("Game").name);
        //SceneManager.UnloadScene(0);
    }

    public static void ExitGame()
    {
        Score.countApples = 0;
        Score.score = 0;
        SceneManager.LoadScene(0);
        //Debug.Log("Выход из игры");
        //Application.Quit();
        ForDatabase.MaxScoreTest = ScoreShow_AND_WinAndOver.maxScore;
    }

}
