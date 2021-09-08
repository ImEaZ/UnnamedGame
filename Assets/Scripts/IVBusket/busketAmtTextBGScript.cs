using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class busketAmtTextBGScript : MonoBehaviour
{
    public RectTransform RT;
    public Text amtText;
    private void Start()
    {
        //RT = GetComponent<RectTransform>();
        //amtText = gameObject.transform.parent.gameObject.GetComponentInChildren<Text>();
    }
    void FixedUpdate()
    {
        RT.SetLeft(37 - ((amtText.text.Length - 1) * 7));
    }
}
