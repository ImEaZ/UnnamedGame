using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropButtonScript : MonoBehaviour
{
    void FixedUpdate()
    {
        var checkBin = FindObjectOfType<GameManager>().binList.Count > 0;
        var isBinBtnShowing = FindObjectOfType<GameManager>().showMoveToBinBtn;
        if (checkBin && isBinBtnShowing == false)
        {
            var dropBtn = gameObject.transform.Find("dropBtn");
            var discardBtn = gameObject.transform.Find("discardBtn");
            dropBtn.transform.gameObject.SetActive(true);
            discardBtn.transform.gameObject.SetActive(true);
        }
        else
        {
            var temp = gameObject.transform.Find("dropBtn");
            var discardBtn = gameObject.transform.Find("discardBtn");
            temp.transform.gameObject.SetActive(false);
            discardBtn.transform.gameObject.SetActive(false);
        }
    }
}
