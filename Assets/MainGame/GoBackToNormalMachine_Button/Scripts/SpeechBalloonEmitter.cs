﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 元に戻す機ボタンが押下されたら、「もとにもどれ！」吹き出しを出すクラス
/// </summary>
public class SpeechBalloonEmitter : MonoBehaviour
{
    /// <summary>
    /// 「もとにもどれ！」吹き出しゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject speechBalloon_Image = default;

    /// <summary>
    /// 元に戻す機ゲームオブジェクト
    /// </summary>
    [SerializeField] GameObject goBackToNormalMachine_Image_Image = default;

    /// <summary>
    /// WaitForSecondsで待機処理がされているかどうか判定するbool変数
    /// </summary>
    /// <remarks>用途：true:WaitForSecondsで待機処理がされている / false:されてない
    /// 意図：何度もCoroutineMoveEnemyAtRandomメソッドを呼ばないようにするため
    /// </remarks>
    bool isWaitForSeconds;

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
        this.speechBalloon_Image.SetActive(false);

        this.isWaitForSeconds = false;
    }

    /// <summary>
    /// 「もとにもどれ！」吹き出しを出す
    /// </summary>
    public void EmitSpeechBalloon()
    {
        // 待機処理(WaitForSeconds)がされていないかつ「もとにもどれ！」吹き出しが半導体に当たっていない場合
        if (this.isWaitForSeconds == false)
        {
            StartCoroutine(CoroutineEmitSpeechBalloon());
        }
    }

    IEnumerator CoroutineEmitSpeechBalloon()
    {
        // 待機処理(WaitForSeconds)が走っている場合はtrueにして
        // 何度もUpdateメソッドの中でStartCoroutine(CoroutineMoveEnemyAtRandom());を呼ばないようにする
        this.isWaitForSeconds = true;

        // 「もとにもとれ！」メッセージボックスの座標を元に戻す機の右隣に描画されるように設定
        this.speechBalloon_Image.transform.position = (Vector2)goBackToNormalMachine_Image_Image.transform.position + new Vector2(3.7f, 0.0f);

        // 非アクティブにしてた「もとにもとれ！」メッセージボックスをアクティブに設定
        this.speechBalloon_Image.SetActive(true);

        // 「もとにもとれ！」を流す
        // ※音声遅延バグあり。
        //this.audioSource.PlayOneShot(this.audioClip);

        // コンマ数秒待機
        // ここでEnemyの動きが指定秒数止まる
        yield return new WaitForSeconds(0.4f);

        // 「もとにもどれ！」メッセージボックスを非アクティブに設定 ←　ここで非アクティブにしていたので、コライドしなくなり、CoroutineGoBackToEachElectronicsが再開しない(yeild returnはもう一度呼び出されたときにそこから再開する処理)
        this.speechBalloon_Image.SetActive(false);

        // 指定秒待機し終えたのでfalseにして吹き出しを出せるようにする。
        this.isWaitForSeconds = false;
    }
}
