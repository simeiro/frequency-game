using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void ChangeToChoices()
    {
        SceneManager.LoadScene("Choices");
    }

    public void ChangeToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public static void ChangeToBarQuiz()
    {
        SceneManager.LoadScene("BarQuiz");
    }

    public static void ChangeToResult()
    {
        SceneManager.LoadScene("Result");
    }

    public void ChangeToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ChangeToCountDown()
    {
        SceneManager.LoadScene("CountDown");
    }

    
}
