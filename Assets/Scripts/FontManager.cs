using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontManager : MonoBehaviour
{
    public Text text;
    public Font forTh, forEn;
    void Start()
    {
        if (FindObjectOfType<GameManager>().Language == "th")
        {
            text.font = forTh;
            text.text = text.text.Split('/')[0];
        }
        else
        {
            text.font = forEn;
            text.text = text.text.Split('/')[1];
        }
        
    }
}
