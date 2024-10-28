using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpectrum : MonoBehaviour
{
    public AudioSpectrum spectrum;
    //オブジェクトの配列（
    [SerializeField] private Transform[] squares;
    //スペクトラムの高さ倍率
    public float scale;

    private void Update()
    {
        int i = 0;

        foreach (var square in squares)
        {
            //オブジェクトのスケールを取得
            var localScale = square.localScale;
            //スペクトラムのレベル＊スケールをYスケールに置き換える
            localScale.y = spectrum.Levels[i] * scale;
            square.localScale = localScale;
            i++;
        }
    }
}