using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVStaminaText : MonoBehaviour
{
    Text staminaText;
    PlayerMovement player;
    void Start()
    {
        staminaText = gameObject.GetComponent<Text>();
        player = FindObjectOfType<GameManager>().playerMovementBridge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        staminaText.text = $"{player.stmSlider.value}/{player.maxStamina}";
    }
}
