using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 「もとにもどれ！」吹き出しが半導体に当たったら半導体が元の電子機器に戻っていくクラス
/// </summary>
public class BackToNormalOrder : MonoBehaviour
{
    /// <summary>
    /// 吹き出しが半導体に当たったら、半導体のランダム移動を止めるフラグ
    /// </summary>
    public static bool flagToStopGameConsoleSemiconductorIfSpeechBalloonHit;

    public static bool flagToStopTVSemiconductorIfSpeechBalloonHit;

    public static bool flagToStopSmartphoneSemiconductorIfSpeechBalloonHit;

    /// <summary>
    /// TVに戻るイベント
    /// </summary>
    public static event EventHandler OnGoingBackToSmartphone;

    /// <summary>
    /// TVに戻るイベント
    /// </summary>
    public static event EventHandler OnGoingBackToTV;

    /// <summary>
    /// ゲーム機に戻るイベント
    /// </summary>
    public static event EventHandler OnGoingBackToGameConsole;

    const string smartphoneSemiconductor = "SmartphoneSemiconductor";
    const string tVSemiconductor = "TVSemiconductor";
    const string gameConsoleSemiconductor = "GameConsoleSemiconductor";

    // Start is called before the first frame update
    void Start()
    {
        flagToStopGameConsoleSemiconductorIfSpeechBalloonHit = false;
        flagToStopTVSemiconductorIfSpeechBalloonHit = false;
        flagToStopSmartphoneSemiconductorIfSpeechBalloonHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tVSemiconductor)
        {
            flagToStopTVSemiconductorIfSpeechBalloonHit = true;

            BackToNormal(collision.gameObject.tag);
        }

        // タグが"SmartphoneSemiconductor"の場合
        if (collision.gameObject.tag == smartphoneSemiconductor)
        {
            flagToStopSmartphoneSemiconductorIfSpeechBalloonHit = true;

            BackToNormal(collision.gameObject.tag);
        }

        if (collision.gameObject.tag == gameConsoleSemiconductor)
        {
            flagToStopGameConsoleSemiconductorIfSpeechBalloonHit = true;

            BackToNormal(collision.gameObject.tag);
        }
    }

    /// <summary>
    /// 半導体を元に戻す
    /// </summary>
    /// <param name="exclamationMark_Text">アクティブにするビックリマーク</param>
    void BackToNormal(string semiconductorName)
    {
        if (OnGoingBackToSmartphone != null && semiconductorName == smartphoneSemiconductor)
        {
            OnGoingBackToSmartphone(this, EventArgs.Empty);
        }

        if (OnGoingBackToTV != null && semiconductorName == tVSemiconductor)
        {
            OnGoingBackToTV(this, EventArgs.Empty);
        }

        if (OnGoingBackToGameConsole != null && semiconductorName == gameConsoleSemiconductor)
        {
            OnGoingBackToGameConsole(this, EventArgs.Empty);
        }
    }
}
