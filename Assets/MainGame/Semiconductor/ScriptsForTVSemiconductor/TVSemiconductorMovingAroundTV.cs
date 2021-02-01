using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 概要：TVの半導体をランダムにTVの周りを移動するクラス
/// </summary>
/// <remarks>
/// </remarks>
public class TVSemiconductorMovingAroundTV : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// WaitForSecondsで待機処理がされているかどうか判定するbool変数
    /// </summary>
    /// <remarks>用途：true:WaitForSecondsで待機処理がされている / false:されてない
    /// 意図：何度もCoroutineMoveEnemyAtRandomメソッドを呼ばないようにするため
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
        // 待機処理(WaitForSeconds)がされていない場合
        if (this.isWaitForSeconds == false)
        {
            StartCoroutine(CoroutineMoveEnemyAtRandom());
        }
    }

    /// <summary>
    /// ランダムで半導体エリア内を一定テンポでワープして移動する
    /// </summary>
    IEnumerator CoroutineMoveEnemyAtRandom()
    {
        // 待機処理(WaitForSeconds)が走っている場合はtrueにして
        // 何度もUpdateメソッドの中でStartCoroutine(CoroutineMoveEnemyAtRandom());を呼ばないようにする
        this.isWaitForSeconds = true;

        // Enemyがランダムに移動する範囲(画面内)を半導体エリア内に設定
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        float xCoordinate = UnityEngine.Random.Range(-6.41f, -0.78f);
        float yCoordinate = UnityEngine.Random.Range(3.22f, 0.35f);

        // 相手エリア内をランダムにワープして移動する
        Vector2 enemyRandomPosition = new Vector2(xCoordinate, yCoordinate);
        this.transform.position = enemyRandomPosition;

        // ここでEnemyの動きが指定秒数止まる
        yield return new WaitForSeconds(0.4f);

        // 指定秒待機し終えたのでfalseにしてEnemyがランダム移動できるようにする。
        this.isWaitForSeconds = false;
    }
    #endregion メソッド
}