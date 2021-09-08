using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    public GameObject interactorImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactorImage.SetActive(true);
            FindObjectOfType<GameManager>().SetInteractTypeAndTurnOnBtn("GoToBed", "GoToBed");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactorImage.SetActive(false);
            FindObjectOfType<GameManager>().interactBtn.SetActive(false);
        }
    }
}
