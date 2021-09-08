using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class showBusketDetailScript : MonoBehaviour
{
    public GameObject textBG, text;
    public Image showImg;
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        #region Swap item
        //var numbers = Regex.Split(gameObject.name, @"\D+").ToList();
        //var checkNumber = numbers.Any(s => !string.IsNullOrWhiteSpace(s));
        //if (checkNumber)
        //{
        //    var ind = Convert.ToInt32(numbers.Where(s => !string.IsNullOrWhiteSpace(s)).FirstOrDefault());
        //    var srcInd = FindObjectOfType<GameManager>().IVSwapSourceIndex;
        //    if (srcInd != -1)
        //    {
        //        FindObjectOfType<GameManager>().registerIVSwapTargetItem(ind - 1);
        //    }
        //}
        #endregion
    }
    private void OnCollisionExit(Collision collision)
    {
        //FindObjectOfType<GameManager>().releaseIVSwapTargetItem();
    }
    void FixedUpdate()
    {
        textBG.SetActive(showImg.enabled);
        text.SetActive(showImg.enabled);
    }
}
