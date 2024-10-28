using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using System.Collections;

public class HZQuiz : MonoBehaviour
{
    private int questionNumber = 0;

    private int numberOfCorrect = 0;

    private int correctIndex;
    
    private GameObject correctButton;

    private int currentSampleHz;

    [SerializeField] private List<GameObject> selects;

    private List<double> selectsHz;

    private GameObject correctImage;

    private GameObject incorectImage;
    // Start is called before the first frame update
    void Start()
    {
        //正解、不正解画像を非表示
        this.correctImage = GameObject.Find("Correct");
        this.incorectImage = GameObject.Find("Incorrect");
        this.correctImage.SetActive(false);
        this.incorectImage.SetActive(false);

        this.currentSampleHz = UnityEngine.Random.Range(0, 10+1);
        this.correctIndex = UnityEngine.Random.Range(0, this.selects.Count);
        this.correctButton = this.selects[correctIndex];
        Debug.Log("正解は" + this.correctButton.name);

        this.selectsHz = new List<double>();
        for(int i = 0; i < this.selects.Count; i++)
        {
            double hz = this.GetEqualTemperament(this.currentSampleHz + 2 * (i + 1));
            hz = Math.Floor(hz);
            selectsHz.Add(hz);
            Button button = this.selects[i].GetComponent<Button>();
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = selectsHz[i] + "Hz";
        }

        Button sampleButton = GameObject.Find("SampleSound").GetComponent<Button>();
        TextMeshProUGUI sampleButtonText = sampleButton.GetComponentInChildren<TextMeshProUGUI>();
        sampleButtonText.text = "SampleSound" + this.GetEqualTemperament(this.currentSampleHz) + "Hz";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClicked()
    {
        if(this.IsCorrect()){this.numberOfCorrect++;}
        // Coroutineを開始
        StartCoroutine(this.AnimationAfterSelection());

        Debug.Log("正解数: " + this.numberOfCorrect);
    }

    public AudioClip GetAudioClip(int number)
    {   //n = 0: ド
        //n = 1: ド＃, レ♭
        //n = 2: レ
        double hz = this.GetEqualTemperament(number);
        int datSamplingRate = 48000;
        double oneCycle = datSamplingRate / hz;
        double halfCycle = oneCycle / 2;
        // 波形を作成
        float[] waveform = new float[datSamplingRate];
        for (int sample = 0; sample < datSamplingRate; sample++)
        {
            waveform[sample] = (sample % oneCycle < halfCycle) ? +1.0f : -1.0f;
        }

        // 波形を AudioClip オブジェクトに格納 (1 秒間あたり 48000 サンプル)
        AudioClip audioClip = AudioClip.Create("TestAudioClip", datSamplingRate, 1, datSamplingRate, false);
        audioClip.SetData(waveform, 0);

        return audioClip;
    }

    public double GetEqualTemperament(int number){
        //n = 0: ド
        //n = 1: ド＃, レ♭
        //n = 2: レ
        return 440 * Math.Pow(Math.Pow(2, number), 1.0 / 12.0);
    }

    public void SetAudioToSampleSound()
    {
        AudioSource audioSrc = new GameObject("SampleAudio").AddComponent<AudioSource>();
        audioSrc.PlayOneShot(this.GetAudioClip(this.currentSampleHz));
    }

    public void SetAudioToQuestionSound()
    {
        AudioSource audioSrc = new GameObject("QuestionAudio").AddComponent<AudioSource>();
        audioSrc.PlayOneShot(this.GetAudioClip(this.currentSampleHz + 2 * (this.correctIndex + 1)));
    }

    private IEnumerator AnimationAfterSelection()
    {
        if(this.IsCorrect())
        {
            this.correctImage.SetActive(true);
        }else
        {
            this.incorectImage.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        // 画像を再度非表示にする
        this.correctImage.SetActive(false);
        this.incorectImage.SetActive(false);
    }

    public bool IsCorrect()
    {
        GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
        return selectedButton == this.correctButton;
    }
}
