using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Text.RegularExpressions;

public class getBusketItemForShow : MonoBehaviour
{
    public Image img;
    public GameObject parentObj;
    public Text amtText;

    void FixedUpdate()
    {
        var temp = FindObjectOfType<GameManager>().busketItemTemp;
        if (temp.Count > 0)
        {
            var numbers = Regex.Split(parentObj.name, @"\D+").ToList();
            var checkNumber = numbers.Any(s => !string.IsNullOrWhiteSpace(s));
            if (checkNumber)
            {
                var ind = Convert.ToInt32(numbers.Where(s => !string.IsNullOrWhiteSpace(s)).FirstOrDefault()) - 1;
                bool checkItem = temp.Any(s => s.index == ind);
                if (checkItem)
                {
                    var item = temp.Where(s => s.index == ind).FirstOrDefault();
                    string name = item.itemName;
                    img.enabled = true;
                    showImage(name);
                    amtText.text = item.qty.ToString();
                }
                else
                {
                    img.enabled = false;
                }
            }
        }
    }
    void showImage(string name)
    {
        switch (name)
        {
            case "stone":
                img.sprite = FindObjectOfType<GameManager>().stone;
                break;
            case "wood":
                img.sprite = FindObjectOfType<GameManager>().wood;
                break;
            case "woodHard":
                img.sprite = FindObjectOfType<GameManager>().hardWood;
                break;
            case "berry":
                img.sprite = FindObjectOfType<GameManager>().berry;
                break;
            case "fiber":
                img.sprite = FindObjectOfType<GameManager>().fiber;
                break;
        }

    }
}
