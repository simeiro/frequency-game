using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class HZBarQuiz : MonoBehaviour
{
    const double divideWidth = 2.0;
    [SerializeField] private int timeLimit;
    [SerializeField] private Slider hzSlider;
    [SerializeField] private GameObject clickBlocker;
    private int sumScore;

    private float elapsedTime;

    private int answerSliderNum;

    private bool questionSoundManage;

    private bool sliderSoundManage;

    private bool answerButtonManage;

    private bool timeup;

    [SerializeField] private TextMeshProUGUI timeLimitText;
    [SerializeField] private TextMeshProUGUI questionText;

    [SerializeField] private TextMeshProUGUI addScoreText;
    [SerializeField] private TextMeshProUGUI sumScoreText;

    [SerializeField] private TextMeshProUGUI sliderHZText;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        sumScore = 0;
        questionSoundManage = true;
        sliderSoundManage = true;
        answerButtonManage = true;
        timeup = false;
        ResetQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){OnAnswerButtonClicked();}
        if(Input.GetKeyDown(KeyCode.D)){OnQuestionSoundButtonClicked();}
        if(Input.GetKeyDown(KeyCode.F)){OnBarSoundButtonClicked();}


        if(timeLimit >= elapsedTime)
        {
            elapsedTime += Time.deltaTime;
            int remainingTime = (int)(timeLimit - elapsedTime);
            timeLimitText.text = "のこり：" + remainingTime.ToString() + "秒";
        }
        else
        {
            if(timeup){return;}
            timeup = true;
            SceneManager.LoadScene("TimeUp");
        }
    }

    public void OnSliderValueChanged()
    {
        sliderHZText.text = (int) GetSliderHz() + "HZ";
    }

    public void OnBarSoundButtonClicked()
    {
        if(!sliderSoundManage){return;}

        AudioSource audio = new GameObject("BarSound").AddComponent<AudioSource>();
        audio.volume = 0.1f;
        audio.pitch = 1.00f;
        audio.PlayOneShot(GetAudioClip(GetSliderHz()));
        Destroy(audio, 1.0f);

        sliderSoundManage = false;
        Invoke("EnablePlaySliderSound", 1.0f);
    }

    public void OnQuestionSoundButtonClicked()
    {
        if(!questionSoundManage){return;}

        AudioSource audio = new GameObject("QuestionSound").AddComponent<AudioSource>();
        audio.volume = 0.1f;
        audio.pitch = 1.00f;
        double answerHz = GetEqualTemperament(answerSliderNum / divideWidth);
        audio.PlayOneShot(GetAudioClip(answerHz));
        Destroy(audio, 1.0f);

        questionSoundManage = false;
        Invoke("EnablePlayQuestionSound", 1.0f);
    }

    public void OnAnswerButtonClicked()
    {
        if(!answerButtonManage){return;}
        answerButtonManage = false;
        clickBlocker.SetActive(true);
        double answerHz = GetEqualTemperament(answerSliderNum / divideWidth);
        questionText.text = (int) answerHz + "HZ";
        sumScore += GetAddScore();
        sumScoreText.text = "スコア：" + sumScore;
        Invoke("ResetQuestion", 2.5f);
    }

    public void ResetQuestion()
    {
        clickBlocker.SetActive(false);
        addScoreText.gameObject.SetActive(false);
        questionText.text = "?HZ";
        answerSliderNum = UnityEngine.Random.Range(-50, 50+1);  
        double answerHz = GetEqualTemperament((int) GetSliderHz() / divideWidth);
        Debug.Log(answerHz);

        answerButtonManage = true;
        OnQuestionSoundButtonClicked();
    }

    public void EnablePlayQuestionSound()
    {
        questionSoundManage = true;
    }

    public void EnablePlaySliderSound()
    {
        sliderSoundManage = true;
    }

    public AudioClip GetAudioClip(double hz)
    {   
        Debug.Log(hz);
        int datSamplingRate = 48000; // サンプリングレート
        int durationInSeconds = 1; // 音の長さ（秒）
        int totalSamples = datSamplingRate * durationInSeconds; // 総サンプル数

        // 周期の計算
        double oneCycle = datSamplingRate / hz; // 1周期あたりのサンプル数
        double halfCycle = oneCycle / 2;

        // 波形を作成
        float[] waveform = new float[totalSamples];
        for (int sample = 0; sample < totalSamples; sample++)
        {
            waveform[sample] = Mathf.Sin(2.0f * Mathf.PI * sample / (float)oneCycle);
        }

        // AudioClipに波形を格納
        AudioClip audioClip = AudioClip.Create("TestAudioClip", totalSamples, 1, datSamplingRate, false);
        audioClip.SetData(waveform, 0);

        return audioClip;
    }

    public double GetEqualTemperament(double number){
        //n = 0: ド
        //n = 1: ド＃, レ♭
        //n = 2: レ
        return 440 * Math.Pow(Math.Pow(2, number), 1.0 / 12.0);
    }

    public int GetAddScore()
    {
        int distance = Math.Abs((int) hzSlider.value - answerSliderNum);
        int addScore;
        if (distance == 0)
        {
            addScoreText.text = "Perfect!  +10";
            addScore = 10;
        }
        else if (distance == 1 || distance == 2)
        {
            addScoreText.text = "Great!  +5";
            addScore = 5;
        }
        else if (distance == 3 || distance == 4)
        {
            addScoreText.text = "Good  +3";
            addScore = 3;
        }
        else if (distance == 5 || distance == 6)
        {
            addScoreText.text = "Bad...  +1";
            addScore = 1;
        }
        else
        {
            addScoreText.text = "Miss...  +0";
            addScore = 0;
        }
        addScoreText.gameObject.SetActive(true);
        return addScore;
    }

    public double GetSliderHz()
    {
        return GetEqualTemperament(((int)hzSlider.value) / divideWidth);
    }

    public int GetSumScore()
    {
        return sumScore;
    }
}
