using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVStaminaSlider : MonoBehaviour
{
    Slider staminaSlider;
    PlayerMovement player;
    void Start()
    {
        staminaSlider = gameObject.GetComponent<Slider>();
        player = FindObjectOfType<GameManager>().playerMovementBridge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        staminaSlider.maxValue = player.maxStamina;
        staminaSlider.value = player.stmSlider.value;
    }
}
