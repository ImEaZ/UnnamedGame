using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpRippleAnim : MonoBehaviour
{
    public RipplePostProcessor rp;
    public void LevelUp(Vector3 playerPosition)
    {
        rp.PlayLevelUpRipple(playerPosition);
    }
}
