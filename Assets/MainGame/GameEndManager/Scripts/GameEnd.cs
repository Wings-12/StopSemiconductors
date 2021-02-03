using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject tVSemiconductor;
    [SerializeField] GameObject smartphoneSemiconductor;

    [SerializeField] GameObject gameClear;

    public static event EventHandler OnStoppingBGM;

    /// <summary>
    /// AudioSourceのインスタンス
    /// </summary>
    [SerializeField] AudioSource audioSource = default;

    bool isOnePlay = false;

    // Update is called once per frame
    void Update()
    {
        if (this.tVSemiconductor == null && this.smartphoneSemiconductor == null)
        {
            if (isOnePlay == false)
            {
                this.gameClear.SetActive(true);

                OnStoppingBGM(this, EventArgs.Empty);

                // ゲームクリア音楽を流す
                this.audioSource.Play();

                isOnePlay = true;
            }
        }
    }
}
