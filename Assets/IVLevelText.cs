using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVLevelText : MonoBehaviour
{
    Text levelText;
    PlayerTemp player;
    void Start()
    {
        levelText = GetComponent<Text>();
        player = FindObjectOfType<GameManager>().player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        levelText.text = player.level.ToString();
    }
}
