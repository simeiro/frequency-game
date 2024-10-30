using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUp : MonoBehaviour
{
    private int waitTime;

    [SerializeField] private TextMeshProUGUI timeUpText;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = 2;
        StartCoroutine(TimeUpCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TimeUpCoroutine()
    {

        // カウントダウンを行うループ
        while (waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
        }
        ChangeScene.ChangeToResult();
        
    }
}
