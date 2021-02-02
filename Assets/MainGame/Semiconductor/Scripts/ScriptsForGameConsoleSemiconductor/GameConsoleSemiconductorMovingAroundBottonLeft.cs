using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 概要：ゲーム機の半導体をランダムに画面左下あたりを移動するクラス
/// </summary>
/// <remarks>
/// </remarks>
public class GameConsoleSemiconductorMovingAroundBottonLeft : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// WaitForSecondsで待機処理がされているかどうか判定するbool変数
    /// </summary>
    /// <remarks>用途：true:WaitForSecondsで待機処理がされている / false:されてない
    /// 意図：何度もCoroutineMove半導体AtRandomメソッドを呼ばないようにするため
    /// </remarks>
    bool isWaitForSeconds;
    #endregion フィールド

    #region メソッド
    // Start is called before the first frame update
    void Start()
    {
        this.isWaitForSeconds = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 半導体を移動する
        MoveSemiconductor();
    }

    /// <summary>
    /// 機能：半導体をランダムに画面内を一定テンポでワープして移動する
    /// </summary>
    void MoveSemiconductor()
    {
        // 待機処理(WaitForSeconds)がされていないかつ「もとにもどれ！」吹き出しが半導体に当たっていない場合
        if (this.isWaitForSeconds == false && BackToNormalOrder.flagToStopGameConsoleSemiconductorIfSpeechBalloonHit == false)
        {
            StartCoroutine(CoroutineMoveSemiconductorAtRandom());
        }
    }

    /// <summary>
    /// ランダムで半導体エリア内を一定テンポでワープして移動する
    /// </summary>
    IEnumerator CoroutineMoveSemiconductorAtRandom()
    {
        // 待機処理(WaitForSeconds)が走っている場合はtrueにして
        // 何度もUpdateメソッドの中でStartCoroutine(CoroutineMove半導体AtRandom());を呼ばないようにする
        this.isWaitForSeconds = true;

        // 半導体がランダムに移動する範囲(画面左側)を半導体エリア内に設定
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        float xCoordinate = UnityEngine.Random.Range(-0.6f, -3.37f);
        float yCoordinate = UnityEngine.Random.Range(-4.43f, -3.07f);

        // 相手エリア内をランダムにワープして移動する
        Vector2 semiconductorRandomPosition = new Vector2(xCoordinate, yCoordinate);
        this.transform.position = semiconductorRandomPosition;

        // ここで半導体の動きが指定秒数止まる
        yield return new WaitForSeconds(0.4f);

        // 指定秒待機し終えたのでfalseにして半導体がランダム移動できるようにする。
        this.isWaitForSeconds = false;
    }
    #endregion メソッド
}