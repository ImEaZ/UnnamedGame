using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var srcInd = FindObjectOfType<GameManager>().IVSwapSourceIndex;
        if (srcInd != -1)
        {
            FindObjectOfType<GameManager>().registerIVSwapTargetItem(-999);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        var srcInd = FindObjectOfType<GameManager>().IVSwapSourceIndex;
        if (srcInd != -1)
        {
            FindObjectOfType<GameManager>().registerIVSwapTargetItem(-999);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        FindObjectOfType<GameManager>().releaseIVSwapTargetItem();
    }
}
