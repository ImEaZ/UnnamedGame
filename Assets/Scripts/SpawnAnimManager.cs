using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimManager : MonoBehaviour
{
    public Animator anim;

    public void ExitAnim()
    {
        anim.SetInteger("Played", 1);
        anim.SetBool("Exit", true);
        anim.enabled = false;
    }
}
