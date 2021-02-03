using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMクラス
/// </summary>
public class BGM : MonoBehaviour
{
    /// <summary>
    /// AudioSourceのインスタンス
    /// </summary>
    [SerializeField] AudioSource audioSource = default;

    // Start is called before the first frame update
    void Start()
    {
        // BGMを流す
        this.audioSource.Play();

        // OnStoppingBGMイベントにOnStoppingBGMEventHandlerを設定
        GameEnd.OnStoppingBGM += StopBGM;
    }

    /// <summary>
    /// BGMを停止するイベントハンドラ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void StopBGM(object sender, System.EventArgs e)
    {
        this.audioSource.Stop();
    }
}
