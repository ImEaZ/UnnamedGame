using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromMainHouseScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && FindObjectOfType<GameManager>().AllowToExitFromMainHouse)
        {
            FindObjectOfType<GameManager>().ExitFromMainHouse();
        }
    }
}
