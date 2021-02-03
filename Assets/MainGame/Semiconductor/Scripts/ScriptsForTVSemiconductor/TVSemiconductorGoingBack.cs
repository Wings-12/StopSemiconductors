using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVSemiconductorGoingBack : MonoBehaviour
{
    /// <summary>
    /// 半導体の戻り先の電子機器
    /// </summary>
    [SerializeField] GameObject electronics = default;

    /// <summary>
    /// 半導体の「！」テキスト
    /// </summary>
    [SerializeField] Text exclamationMark_Text = default;

    /// <summary>
    /// 半導体を元に戻した場合に切り替えるスプライト
    /// </summary>
    [SerializeField] Sprite changingSprite = default;

    ///<summary>
    ///半導体のrigidbody
    ///</summary>
    [SerializeField] Rigidbody2D semiconductorRigidbody2D = default;

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
        BackToNormalOrder.OnGoingBackToTV += GoBackToElectronics;

        this.exclamationMark_Text.enabled = false;

        this.isWaitForSeconds = false;
    }

    private void Update()
    {
        StopSemiconductor(this.transform.position, this.electronics.transform.position, this.semiconductorRigidbody2D);
    }

    void GoBackToElectronics(object sender, System.EventArgs e)
    {
        // 非表示の「！」を表示
        this.exclamationMark_Text.enabled = true;

        if (this.isWaitForSeconds == false)
        {
            StartCoroutine(CoroutineGoBackToEachElectronics(this.transform.position, this.electronics.transform.position, this.semiconductorRigidbody2D));
        }
    }

    IEnumerator CoroutineGoBackToEachElectronics(Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D semiconductorRigidbody2D)
    {
        // 待機処理(WaitForSeconds)が走っている場合はtrueにして
        // 何度もUpdateメソッドの中でStartCoroutine(CoroutineMoveEnemyAtRandom());を呼ばないようにする
        this.isWaitForSeconds = true;

        // 半導体がビックリしているため指定秒数止まる
        yield return new WaitForSeconds(1.0f);

        GoBackToEachElectronics(semiconductorPosition, originalPosition);

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
    protected void GoBackToEachElectronics(Vector3 semiconductorPosition, Vector3 originalPosition)
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
    protected void StopSemiconductor(Vector3 semiconductorPosition, Vector3 originalPosition, Rigidbody2D semiconductorRigidbody2D)
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
            this.transform.position = originalPosition;
            // ------------------------------------------------

            // 半導体の動きを停止する
            semiconductorRigidbody2D.velocity = Vector2.zero;

            // 半導体を電子機器の中に入れる
            Destroy(this.gameObject);

            // 電子機器の電源が入る
            this.electronics.GetComponent<Image>().sprite = changingSprite;
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
        semiconductorRigidbody2D.velocity = velocityOfMoveDestination;
    }
}
