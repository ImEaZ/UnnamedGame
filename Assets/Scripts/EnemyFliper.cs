using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFliper : MonoBehaviour
{
    public AIPath aiPath;
    public SpriteRenderer spriteR;
    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            spriteR.flipX = true;
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            spriteR.flipX = false;
        }
    }
}
