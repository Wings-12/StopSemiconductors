using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    /// <summary>
    /// AudioClipのインスタンス
    /// </summary>
    [SerializeField] AudioClip audioClip = default;

    /// <summary>
    /// AudioSourceのインスタンス
    /// </summary>
    [SerializeField] AudioSource audioSource = default;

    public void TestAudio()
    {
        this.audioSource.PlayOneShot(this.audioClip);
    }
}
