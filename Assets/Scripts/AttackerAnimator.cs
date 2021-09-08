using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttacker;

public class AttackerAnimator : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer spr;
    bool lockAnim = false;
    public void PlaySwordEffect()
    {
        if (lockAnim)
            return;
        lockAnim = true;
        transform.position = transform.parent.position;
        switch (PlayerAttacker.attackDirection)
        {
            case AttackDirection.aboveFlipRight:
                spr.flipX = false;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + 0.09f, transform.position.y, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                break;
            case AttackDirection.aboveRight:
                spr.flipX = false;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.078f, transform.position.y + 0.222f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.back, 41f);
                break;
            case AttackDirection.right:
                spr.flipX = false;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + 0.08f, transform.position.y + 0.26f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.back, 90f);
                break;
            case AttackDirection.belowRight:
                spr.flipX = false;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.294f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.back, 120f);
                break;
            case AttackDirection.belowFlipRight:
                spr.flipX = false;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + 0.22f, transform.position.y + 0.41f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.forward, 185f);
                break;
            case AttackDirection.belowFlipLeft:
                spr.flipX = true;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.31f, transform.position.y + 0.407f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.forward, 175f);
                break;
            case AttackDirection.belowLeft:
                spr.flipX = true;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.16f, transform.position.y + 0.321f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.forward, 132f);
                break;
            case AttackDirection.left:
                spr.flipX = true;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.05f, transform.position.y + 0.21f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.forward, 86.14f);
                break;
            case AttackDirection.aboveLeft:
                spr.flipX = true;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.08f, transform.position.y + 0.222f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                transform.Rotate(Vector3.forward, 41f);
                break;
            case AttackDirection.aboveFlipLeft:
                spr.flipX = true;
                spr.flipY = false;
                transform.position = new Vector3(transform.position.x + -0.11f, transform.position.y + 0.043f, transform.position.z);
                transform.localRotation = new Quaternion(0, 0, 0, transform.rotation.w);
                break;
        }
        playAnim();
    }
    public void UnlockAnim()
    {
        stopAnim();
        lockAnim = false;
    }
    void playAnim()
    {
        anim.SetBool("Play", true);
        anim.SetBool("Exit", false);
    }
    void stopAnim()
    {
        anim.SetBool("Play", false);
        anim.SetBool("Exit", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAI"))
        {
            var gotHiter = collision.gameObject.GetComponent<GotHitFromPlayerDetector>();
            gotHiter.GotHitFromPlayer();
        }
    }


}
