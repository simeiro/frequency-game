using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class GetAudioData : MonoBehaviour
{
    public enum FFT_Resolution
    {
        _8192 = 8192, _4096 = 4096, _2048 = 2048, _1024 = 1024, _512 = 512, _256 = 256, _128 = 128, _64 = 64
    }

    [Space]
    [Tooltip("64-8192の間の2の累乗の数字である必要がある")]
    public FFT_Resolution FFT_res = FFT_Resolution._512;
    [SerializeField] private AudioSource source;
    [SerializeField] private int dataOffset = 0;
    [Tooltip("高速フーリエ変換の窓関数指定")]
    [SerializeField] private FFTWindow FFT_wf = FFTWindow.Triangle;
    [HideInInspector] public float[] spectrumData = null;
    private float[] data;

    private void OnEnable()
    {
        //準備
        var clip = source.clip;
        data = new float[clip.channels * clip.samples];
        source.clip.GetData(data, dataOffset);
        spectrumData = new float[(int)FFT_res];
    }

    public void FixedUpdate()
    {
        Refresh();
    }

    private void Refresh()
    {
        bool cond = source.isPlaying && source.timeSamples < data.Length;

        //周波数成分取得
        if (cond) source.GetSpectrumData(spectrumData, 0, FFT_wf);
        else spectrumData = Enumerable.Repeat<float>(0, (int)FFT_res).ToArray();
    }
}
