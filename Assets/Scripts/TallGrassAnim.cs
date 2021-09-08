using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrassAnim : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Exit", false);
            anim.SetBool("Play", true);
        }
    }

    public void ExitAnim()
    {
        anim.SetBool("Play", false);
        anim.SetBool("Exit", true);
    }
}
