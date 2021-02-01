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
    [SerializeField] GameObject tvSemiconductor;
    [SerializeField] GameObject smartphoneSemiconductor;
    [SerializeField] GameObject semiconductor2;

    [SerializeField] GameObject tv;
    [SerializeField] GameObject smartphone;
    [SerializeField] GameObject gameConsole;

    [SerializeField] Text exclamationMark_Text;
    [SerializeField] Text smartphoneSemiconductor_exclamationMark_Text;
    [SerializeField] Text exclamationMark_Text2;

    ///<summary>
    ///半導体のrigidbody
    ///</summary>
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] Rigidbody2D smartphoneSemiconductor_rigidbody2D1;
    [SerializeField] Rigidbody2D rigidbody2D2;

    public static bool smartphoneSemiconductorCollisionFlag;

    /// <summary>
    /// それぞれの電子機器に戻るイベント
    /// </summary>
    public event EventHandler OnGoingBackToEachElectronics;

    /// <summary>
    /// WaitForSecondsで待機処理がされているかどうか判定するbool変数
    /// </summary>
    /// <remarks>用途：true:WaitForSecondsで待機処理がされている / false:されてない
    /// 意図：何度もCoroutineMoveEnemyAtRandomメソッドを呼ばないようにするため
    /// </remarks>
    bool isWaitForSeconds;

    // Start is called before the first frame update
    void Start()
    {
        this.exclamationMark_Text.enabled = false;
        this.smartphoneSemiconductor_exclamationMark_Text.enabled = false;
        this.exclamationMark_Text2.enabled = false;

        smartphoneSemiconductorCollisionFlag = false;

        this.isWaitForSeconds = false;
    }

    private void Update()
    {
        StopSemiconductor(this.smartphoneSemiconductor.transform.position, this.smartphone.transform.position, this.smartphoneSemiconductor_rigidbody2D1); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TVSemiconductor")
        {
            //BackToNormal(exclamationMark_Text1, this.tvSemiconductor.transform.position, this.smartphone.transform.position, this.rigidbody2D1);

            // 半導体が対応する電子機器に戻る ：
            //↓
            // 型：void
            //引数：
            //1.この半導体のワールド座標
            //2.対応電子機器のワールド座標
            //処理：
            //-コンマ数秒待機
            //- 比較的ゆっくり対応電子機器へ直線的に移動
            //- 対応電子機器と衝突したら、その半導体を削除
            //- イベント(ElectronicsFixed)発生させて、対応電子機器の電源を入れる。

            //※どうやって半導体と電子機器を紐づけるか？
            //→ 必要ない？ 理由：上のメソッド作れば問題なく対応電子機器へ移動するから。
        }

        // タグが"SmartphoneSemiconductor"かつまだ「もとにもどれ！」吹き出し」と衝突していない場合
        if (collision.gameObject.tag == "SmartphoneSemiconductor" && smartphoneSemiconductorCollisionFlag == false)
        {
            smartphoneSemiconductorCollisionFlag = true;

            BackToNormal(smartphoneSemiconductor_exclamationMark_Text, this.smartphoneSemiconductor.transform.position, this.smartphone.transform.position, this.smartphoneSemiconductor_rigidbody2D1);
        }

        if (collision.gameObject.tag == "Semiconductor2")
        {
            //BackToNormal(exclamationMark_Text2, this.semiconductor2.transform.position, this.gameConsole.transform.position, this.rigidbody2D2);
        }
    }

    /// <summary>
    /// 半導体を元に戻す
    /// </summary>
    /// <param name="exclamationMark_Text">アクティブにするビックリマーク</param>
    /// <param name="semiconductorPosition">半導体の座標</param>
    /// <param name="originalPosition">半導体が戻る電子機器の座標</param>
    void BackToNormal(Text exclamationMark_Text, Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D sericonductorRigidbocy2D)
    {
        // 非表示の「！」を表示
        exclamationMark_Text.enabled = true;

        //if (this.OnGoingBackToEachElectronics != null)
        //{
        //    OnGoingBackToEachElectronics(this, EventArgs.Empty);
        //}

        if (this.isWaitForSeconds == false)
        {
            StartCoroutine(CoroutineGoBackToEachElectronics(semiconductorPosition, originalPosition, sericonductorRigidbocy2D));
        }
    }

    IEnumerator CoroutineGoBackToEachElectronics(Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D sericonductorRigidbocy2D)
    {
        // 待機処理(WaitForSeconds)が走っている場合はtrueにして
        // 何度もUpdateメソッドの中でStartCoroutine(CoroutineMoveEnemyAtRandom());を呼ばないようにする
        this.isWaitForSeconds = true;

        // 半導体がビックリしているため指定秒数止まる
        yield return new WaitForSeconds(1.0f);

        GoBackToEachElectronics(semiconductorPosition, originalPosition, sericonductorRigidbocy2D);

        // 処理が終わったのでフラグを降ろす
        this.isWaitForSeconds = false;
    }

    /// <summary>
    /// 機能：半導体を対応するそれぞれの電子機器へ移動する
    /// 
    /// 引数：なし
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：忘れた。
    protected void GoBackToEachElectronics(Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D sericonductorRigidbocy2D)
    {
        // 移動方向(°)
        float moveAngle = 0.0f;
        moveAngle = GetAngleOf_moveDestination(semiconductorPosition, originalPosition);

        // 移動方向に対して半導体を動かす
        SetVelocityForRigidbody2D(moveAngle, 10.0f);
    }

    /// <summary>
    /// 機能：半導体をタッチした位置に近づいたら止める
    /// 
    /// 引数：なし
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：忘れた。
    protected void StopSemiconductor(Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D sericonductorRigidbocy2D)
    {
        // 各電子機器位置からの差
        float touchDif = 0.5f;

        // 半導体が各電子機器を原点とする矩形以内に入った場合
        if ((
            (originalPosition.x - touchDif <= semiconductorPosition.x) && (semiconductorPosition.x <= originalPosition.x + touchDif)
            )
            &&
            (
            (originalPosition.y - touchDif <= semiconductorPosition.y) && (semiconductorPosition.y <= originalPosition.y + touchDif)
            ))
        {
            // 半導体の座標を各電子機器の座標に設定
            semiconductorPosition = originalPosition;

            // ※現在スマホの半導体のみ制御中。
            this.smartphoneSemiconductor.transform.position = originalPosition;
            // ------------------------------------------------

            // 半導体の動きを停止する
            sericonductorRigidbocy2D.velocity = Vector2.zero;

            // スマホの電源が入る

        }
        else
        {
            Debug.Log("スマホ半導体の座標は：" + semiconductorPosition);
        }
    }

    /// <summary>
    /// 機能：現在の半導体の座標から移動先の座標への角度を求める
    /// 
    /// 引数：
    /// 引数1：現在の半導体の座標
    /// 引数2：移動先の座標
    /// 
    /// 戻り値：2点の角度(°)
    /// 
    /// 備考：参考サイト：https://qiita.com/2dgames_jp/items/60274efb7b90fa6f986a
    /// </summary>
    public float GetAngleOf_moveDestination(Vector2 currentCharacterPosition, Vector2 moveDestination)
    {
        // 隣辺
        float adjacent = moveDestination.x - currentCharacterPosition.x;

        // 対辺
        float opposite = moveDestination.y - currentCharacterPosition.y;

        // 角度(ラジアン)
        float rad = Mathf.Atan2(opposite, adjacent);

        // 角度(°)
        return rad * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 機能：ゲームオブジェクトにアタッチされたRigidbody2D速度を設定する
    /// 
    /// 引数：
    /// 引数1：現在の半導体の座標から移動先の座標への角度
    /// 引数2：半導体の移動スピード
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：https://qiita.com/2dgames_jp/items/60274efb7b90fa6f986a
    /// </summary>
    public void SetVelocityForRigidbody2D(float angleOfMoveDestination, float speed)
    {
        // 移動先までの速度
        Vector2 velocityOfMoveDestination;

        // 移動先座標への角度におけるcosを取得
        velocityOfMoveDestination.x = Mathf.Cos(Mathf.Deg2Rad * angleOfMoveDestination) * speed;

        // 移動先座標への角度におけるsinを取得
        velocityOfMoveDestination.y = Mathf.Sin(Mathf.Deg2Rad * angleOfMoveDestination) * speed;

        // tan(斜辺の長さ(大きさ)とその向き == (cos(x座標), sin(y座標)))
        rigidbody2D.velocity = velocityOfMoveDestination;
    }
}
