using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHouseInteracting : MonoBehaviour
{
    public GameObject interactImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactImage.SetActive(true);
            FindObjectOfType<GameManager>().SetInteractTypeAndTurnOnBtn("Touch", "EnterMainHouse");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactImage.SetActive(false);
            FindObjectOfType<GameManager>().interactBtn.SetActive(false);
            FindObjectOfType<GameManager>().SetActiveActionSwitchBtn(true);
        }
    }
}
