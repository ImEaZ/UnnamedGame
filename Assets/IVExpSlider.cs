using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVExpSlider : MonoBehaviour
{
    Slider expSlider;
    PlayerTemp player;
    int maxStackLevel = 0;
    void Start()
    {
        maxStackLevel = FindObjectOfType<GameManager>().maxStackLevel;
        player = FindObjectOfType<GameManager>().player;
        expSlider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        expSlider.maxValue = maxStackLevel;
        expSlider.value = player.exp;
    }
    public void UpdateMaxStackLevel(int stackLevel)
    {
        //player = FindObjectOfType<GameManager>().player;
        maxStackLevel = stackLevel;
    }
}
