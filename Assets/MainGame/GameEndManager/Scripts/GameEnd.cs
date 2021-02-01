using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject tVSemiconductor;
    [SerializeField] GameObject smartphoneSemiconductor;

    [SerializeField] GameObject gameClear;

    // Update is called once per frame
    void Update()
    {
        if (this.tVSemiconductor == null && this.smartphoneSemiconductor == null)
        {
            this.gameClear.SetActive(true);
        }
    }
}
