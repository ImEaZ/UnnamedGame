using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeOutItemLogUI : MonoBehaviour
{
    bool fadingOut = false;
    public float alpha = 1f;
    void Start()
    {
        Invoke("startFading", 6f);
    }
    void FixedUpdate()
    {
        if (fadingOut)
        {
            var newColor = Color.white;
            alpha -= 0.005f;
            newColor.a = alpha;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var gameObj = gameObject.transform.GetChild(i).gameObject;
                if (gameObj.name == "itemImage")
                {
                    gameObj.GetComponent<Image>().color = newColor;
                }
                else
                {
                    gameObj.GetComponent<Text>().color = newColor;
                }
            }
            if (alpha <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
    void startFading()
    {
        fadingOut = true;
    }
    public void resetFading()
    {
        fadingOut = false;
        CancelInvoke("startFading");
        Invoke("startFading", 6f);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var gameObj = gameObject.transform.GetChild(i).gameObject;
            if (gameObj.name == "itemImage")
            {
                gameObj.GetComponent<Image>().color = Color.white;
            }
            else
            {
                gameObj.GetComponent<Text>().color = Color.white;
            }
        }
    }
}
