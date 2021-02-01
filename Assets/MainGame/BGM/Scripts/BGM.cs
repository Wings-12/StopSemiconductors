using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMクラス
/// </summary>
public class BGM : MonoBehaviour
{
    /// <summary>
    /// AudioClipのインスタンス
    /// </summary>
    [SerializeField] AudioClip audioClip = default;

    /// <summary>
    /// AudioSourceのインスタンス
    /// </summary>
    [SerializeField] AudioSource audioSource = default;

    // Start is called before the first frame update
    void Start()
    {
        // BGMを流す
        this.audioSource.PlayOneShot(this.audioClip);

        // OnStoppingBGMイベントにOnStoppingBGMEventHandlerを設定
        //this.enemyController.OnStoppingBGM += OnStoppingBGMEventHandler;
    }

    /// <summary>
    /// BGMを停止するイベントハンドラ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void OnStoppingBGMEventHandler(object sender, System.EventArgs e)
    {
        this.audioSource.Stop();
    }
}
