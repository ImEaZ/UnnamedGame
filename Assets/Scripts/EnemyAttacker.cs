using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    public CircleCollider2D attacker;
    public float AttackerRadius;
    private void FixedUpdate()
    {
        if (attacker.radius != AttackerRadius)
        {
            attacker.radius = AttackerRadius;
        }
    }
    public void ActiveAttacker(bool boolVal)
    {
        attacker.enabled = boolVal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !FindObjectOfType<GameManager>().playerMovementBridge.playerInvisibled)
        {
            FindObjectOfType<GameManager>().playerMovementBridge.playerGotHit = true;
        }
    }

}
