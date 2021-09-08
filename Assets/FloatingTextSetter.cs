using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextSetter : MonoBehaviour
{
    public GameObject floatingTextObj;
    public void SetTextAndShow(string text)
    {
        floatingTextObj.GetComponent<TextMeshPro>().text = text;
        //Debug.Log(text);
        floatingTextObj.SetActive(true);
    }
}
