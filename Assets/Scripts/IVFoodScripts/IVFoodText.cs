using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IVFoodText : MonoBehaviour
{
    Text foodText;
    PlayerMovement player;
    void Start()
    {
        foodText = gameObject.GetComponent<Text>();
        player = FindObjectOfType<GameManager>().playerMovementBridge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foodText.text = $"{player.foodSlider.value}/{player.maxFood}";
    }
}
