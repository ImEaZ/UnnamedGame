using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Text.RegularExpressions;

public class getItemForShow : MonoBehaviour
{
    public Image img;
    public GameObject parentObj;
    public Text amtText;
    private void Start()
    {
        //parentObj = gameObject.transform.parent.gameObject;
        //img = gameObject.GetComponent<Image>();
        //amtText = parentObj.GetComponentInChildren<Text>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var temp = FindObjectOfType<GameManager>().player;
        if (temp != null)
        {
            var numbers = Regex.Split(parentObj.name, @"\D+").ToList();
            var checkNumber = numbers.Any(s => !string.IsNullOrWhiteSpace(s));
            if (checkNumber)
            {
                var ind = Convert.ToInt32(numbers.Where(s => !string.IsNullOrWhiteSpace(s)).FirstOrDefault()) - 1;
                bool checkItem = temp.inventoryList.Any(s => s.index == ind);
                if (checkItem)
                {
                    var item = temp.inventoryList.Where(s => s.index == ind).FirstOrDefault();
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
