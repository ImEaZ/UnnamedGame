using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class craftTableScript : MonoBehaviour
{
    public GameObject Lv1, Lv2;
    public GameObject interactorImage;
    void Update()
    {
        var craftTableLvTemp = FindObjectOfType<GameManager>().player.craftTableLevel;
        if (craftTableLvTemp > 1 && !Lv2.activeSelf)
        {
            Lv2.SetActive(true);
        }
        else if (craftTableLvTemp > 0 && !Lv1.activeSelf)
        {
            Lv1.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactorImage.SetActive(true);
            FindObjectOfType<GameManager>().SetInteractTypeAndTurnOnBtn("Hammer", "CraftTable");
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
