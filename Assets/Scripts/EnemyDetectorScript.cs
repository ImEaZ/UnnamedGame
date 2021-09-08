using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorScript : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAI"))
        {
            //FindObjectOfType<GameManager>().playerMovementBridge.EnemyDetected();
        }
    }

}
