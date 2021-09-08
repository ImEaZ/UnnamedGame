using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class binAmountScript : MonoBehaviour
{
    void FixedUpdate()
    {
        var binCount = FindObjectOfType<GameManager>().binList.Count;
        var checkBin = binCount > 0;
        if (checkBin)
        {
            var BinAmount = gameObject.transform.Find("BinAmount");
            BinAmount.gameObject.SetActive(true);
            BinAmount.GetComponentInChildren<Text>().text = binCount.ToString();
        }
        else
        {
            var BinAmount = gameObject.transform.Find("BinAmount");
            BinAmount.gameObject.SetActive(false);
        }
    }
}
