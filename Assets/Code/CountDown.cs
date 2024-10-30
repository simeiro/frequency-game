using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{

    private int countDown;

    [SerializeField] private TextMeshProUGUI countDownText;

    // Start is called before the first frame update
    void Start()
    {
        countDown = 3;
        StartCoroutine(CountDownCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountDownCoroutine()
    {

        // カウントダウンを行うループ
        while (countDown > 0)
        {
            // テキストにカウントダウンの値を表示
            countDownText.text = countDown.ToString();
        
            yield return new WaitForSeconds(1);

            countDown--;
        }
        ChangeScene.ChangeToBarQuiz();
    }
}
