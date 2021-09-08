using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBridge : MonoBehaviour
{
    public EnemyAI enemyAI;
    public EnemyAttacker attacker;
    public void UnlockMovement()
    {
        enemyAI.UnlockMoving();
    }

    public void Hurting()
    {
        enemyAI.GotHit++;
        if (enemyAI.GotHit >= enemyAI.HP)
        {
            enemyAI.Dead();
            return;
        }
        enemyAI.hurting = true;
    }

    public void HurtingEnd()
    {
        enemyAI.hurting = false;
    }

    public void Attack()
    {
        attacker.ActiveAttacker(true);
    }

    public void Unattack()
    {
        attacker.ActiveAttacker(false);
    }
}
