using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartManage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button rankingButton;
    [SerializeField] private Image logoImage;
    [SerializeField] private AudioSource bgm;

    // [SerializeField] private GameObject audioManager;
    // [SerializeField] private GameObject audioSpectrumManager;
    // [SerializeField] private GameObject squareSpectrum;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClicked()
    {
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        tutorialButton.gameObject.SetActive(false);
        // rankingButton.gameObject.SetActive(false);
        logoImage.gameObject.SetActive(false);
        bgm.Stop();
    }

    public void OnTutorialButtonClicked()
    {
        // DontDestroyOnLoad(audioManager);
        // DontDestroyOnLoad(audioSpectrumManager);
        // DontDestroyOnLoad(squareSpectrum);
    }

    public AudioClip GetAudioClip(double hz)
    {   
        Debug.Log(hz);
        int datSamplingRate = 48000; // サンプリングレート
        double durationInSeconds = 0.7; // 音の長さ（秒）
        int totalSamples = (int)((double)datSamplingRate * durationInSeconds); // 総サンプル数

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
}
