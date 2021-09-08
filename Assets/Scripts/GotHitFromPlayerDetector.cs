using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotHitFromPlayerDetector : MonoBehaviour
{
    public BasicSlimeAIBridge BasicEnemyAIBridge;
    public EnemyAIBridge CarrotEnemyAIBridge;
    public void GotHitFromPlayer()
    {
        if (gameObject.name.Contains("BasicSlime"))
        {
            BasicEnemyAIBridge.Hurting();
        }
        else if (gameObject.name.Contains("Carrot"))
        {
            CarrotEnemyAIBridge.Hurting();
        }
    }
}
