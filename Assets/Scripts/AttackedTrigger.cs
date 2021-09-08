using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAI"))
        {
            var gotHiter = collision.gameObject.GetComponent<GotHitFromPlayerDetector>();
            gotHiter.GotHitFromPlayer();
        }
    }
}
