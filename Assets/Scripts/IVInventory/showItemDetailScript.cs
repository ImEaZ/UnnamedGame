using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;
using System.Linq;

public class showItemDetailScript : MonoBehaviour
{
    public GameObject textBG, text;
    public Image showImg;
    private void Start()
    {
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var temp = transform.GetChild(i).gameObject;
            if (temp.name == "textBG")
            {
                textBG = temp;
            }
            else if (temp.name == "amtText")
            {
                text = temp;
            }
            else
            {
                showImg = temp.GetComponent<Image>();
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        #region Swap item
        var numbers = Regex.Split(gameObject.name, @"\D+").ToList();
        var checkNumber = numbers.Any(s => !string.IsNullOrWhiteSpace(s));
        if (checkNumber)
        {
            var ind = Convert.ToInt32(numbers.Where(s => !string.IsNullOrWhiteSpace(s)).FirstOrDefault());
            var srcInd = FindObjectOfType<GameManager>().IVSwapSourceIndex;
            if (srcInd != -1)
            {
                FindObjectOfType<GameManager>().registerIVSwapTargetItem(ind - 1);
            }
        }
        #endregion
    }
    private void OnCollisionExit(Collision collision)
    {
        FindObjectOfType<GameManager>().releaseIVSwapTargetItem();
    }
    void FixedUpdate()
    {
        textBG.SetActive(showImg.enabled);
        text.SetActive(showImg.enabled);
    }
}
