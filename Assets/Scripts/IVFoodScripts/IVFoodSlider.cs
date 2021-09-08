using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVFoodSlider : MonoBehaviour
{
    Slider foodSlider;
    PlayerMovement player;
    void Start()
    {
        foodSlider = gameObject.GetComponent<Slider>();
        player = FindObjectOfType<GameManager>().playerMovementBridge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foodSlider.maxValue = player.maxFood;
        foodSlider.value = player.foodSlider.value;
    }
}
