using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject barQuiz = GameObject.Find ("BarQuizManager");
        int score = barQuiz.GetComponent<HZBarQuiz>().GetSumScore();
        Destroy(barQuiz);
        Debug.Log(score);
        scoreText.text = "Your score" + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
