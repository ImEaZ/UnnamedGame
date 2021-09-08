using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVExpText : MonoBehaviour
{
    Text expText;
    PlayerTemp player;
    int maxStackLevel = 0;
    void Start()
    {
        maxStackLevel = FindObjectOfType<GameManager>().maxStackLevel;
        player = FindObjectOfType<GameManager>().player;
        expText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        expText.text = $"{player.exp}/{maxStackLevel}";
    }
    
    public void UpdateMaxStackLevel(int stackLevel)
    {
        //player = FindObjectOfType<GameManager>().player;
        maxStackLevel = stackLevel;
    }
}
