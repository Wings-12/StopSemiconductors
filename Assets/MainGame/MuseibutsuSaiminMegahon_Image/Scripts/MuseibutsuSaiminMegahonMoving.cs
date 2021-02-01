using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI; // touch.phaseデバッグのため臨時で入れた。 2020/09/08

/// <summary>
/// 画面上の無生物催眠メガホンをタッチ操作で移動できるクラス
/// </summary>
public class MuseibutsuSaiminMegahonMoving : MonoBehaviour
{
    #region フィールド
    ///<summary>
    ///無生物催眠メガホンのrigidbody
    ///</summary>
    protected Rigidbody2D rigidbody2D;

    ///<summary>
    ///無生物催眠メガホンを描画する座標(1マス右側に描画されている無生物催眠メガホンの移動先座標)
    ///</summary>
    protected Vector2 drawingPosition;

    ///<summary>
    ///無生物催眠メガホンの移動スピード
    ///</summary>
    protected float characterSpeed;

    readonly Vector2 positionToMake_drawingPosition = new Vector2(2.5f, 0.0f);
    #endregion

    // Start is called before the first frame update
    protected void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();

        this.transform.position = Vector2.zero;

        this.drawingPosition = Vector2.zero;

        this.characterSpeed = 28.0f;
    }

    // Update is called once per frame
    protected void Update()
    {
        Update_touchPosition();

        MoveCharacter();
        StopCharacter(); 
    }

    /// <summary>
    /// 機能：無生物催眠メガホンをスマホタッチ操作で移動する
    /// 
    /// 引数：なし
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：忘れた。
    protected void MoveCharacter()
    {
        // ここに無生物催眠メガホンのボタンを押したときは無生物催眠メガホンを移動しないように条件文を追加する
        if (Input.touchCount > 0)
        {
            // 移動方向(°)
            float moveAngle = 0.0f;
            moveAngle = GetAngleOf_moveDestination(this.transform.position, this.drawingPosition);
            //moveAngle = GetAngleOf_moveDestination(this.transform.position, this.touchPosition + new Vector2(1.6f, 0.0f, 0.0f));

            // 移動方向に対して無生物催眠メガホンを動かす
            SetVelocityForRigidbody2D(moveAngle, characterSpeed);
        }
    }

    /// <summary>
    /// 機能：無生物催眠メガホンの移動先のタッチした座標をフレーム毎に更新する
    /// 
    /// 引数：なし
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：忘れた。
    /// </summary>
    protected void Update_touchPosition()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // タッチ座標を更新
            Vector2 touchPosition = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);

            // 無生物催眠メガホンのボタンの左上の角のx座標より左の領域をタッチした場合
            if (touchPosition.x < 7.3f)
            {
                // 描画座標を更新
                this.drawingPosition = touchPosition + this.positionToMake_drawingPosition;
            }
        }
    }

    /// <summary>
    /// 機能：無生物催眠メガホンをタッチした位置に近づいたら止める
    /// 
    /// 引数：なし
    /// 
    /// 戻り値：なし
    /// 
    /// 備考：参考サイト：忘れた。
    protected void StopCharacter()
    {
        // タッチ位置からの差
        float touchDif = 0.5f;

        // 無生物催眠メガホンがタッチ位置を原点とする矩形以内に入った場合
        if ((
            (this.drawingPosition.x - touchDif <= this.transform.position.x) && (this.transform.position.x <= this.drawingPosition.x + touchDif)
            )
            &&
            (
            (this.drawingPosition.y - touchDif <= this.transform.position.y) && (this.transform.position.y <= this.drawingPosition.y + touchDif)
            ))
        {
            // 無生物催眠メガホンの位置をタッチ位置の右側に設定する
            // 理由：無生物催眠メガホンがタッチ位置と被らないようにするため
            this.transform.position = this.drawingPosition;

            // 無生物催眠メガホンの動きを停止する
            this.rigidbody2D.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 機能：現在の無生物催眠メガホンの座標から移動先の座標への角度を求める
    /// 
    /// 引数：
    /// 引数1：現在の無生物催眠メガホンの座標
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
    /// 引数1：現在の無生物催眠メガホンの座標から移動先の座標への角度
    /// 引数2：無生物催眠メガホンの移動スピード
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
