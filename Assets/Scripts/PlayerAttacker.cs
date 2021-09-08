using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public static AttackDirection attackDirection;
    //public static BoxCollider2D attacker;
    public GameObject aboveFlipRight, aboveRight, right, belowRight, belowFlipRight, belowFlipLeft, belowLeft, left, aboveLeft, aboveFlipLeft;
    private void Start()
    {
        //attacker = GetComponent<BoxCollider2D>();
        //attacker.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyAI"))
        {
            var gotHiter = collision.gameObject.GetComponent<GotHitFromPlayerDetector>();
            gotHiter.GotHitFromPlayer();
        }
    }

    public void attackEnemy()
    {
        switch (attackDirection)
        {
            case AttackDirection.aboveFlipRight:
                aboveFlipRight.SetActive(true);
                Invoke("disableAttacker_aboveFlipRight", 0.2f);
                break;
            case AttackDirection.aboveRight:
                aboveRight.SetActive(true);
                Invoke("disableAttacker_aboveRight", 0.2f);
                break;
            case AttackDirection.right:
                right.SetActive(true);
                Invoke("disableAttacker_right", 0.2f);
                break;
            case AttackDirection.belowRight:
                belowRight.SetActive(true);
                Invoke("disableAttacker_belowRight", 0.2f);
                break;
            case AttackDirection.belowFlipRight:
                belowFlipRight.SetActive(true);
                Invoke("disableAttacker_belowFlipRight", 0.2f);
                break;
            case AttackDirection.belowFlipLeft:
                belowFlipLeft.SetActive(true);
                Invoke("disableAttacker_belowFlipLeft", 0.2f);
                break;
            case AttackDirection.belowLeft:
                belowLeft.SetActive(true);
                Invoke("disableAttacker_belowLeft", 0.2f);
                break;
            case AttackDirection.left:
                left.SetActive(true);
                Invoke("disableAttacker_left", 0.2f);
                break;
            case AttackDirection.aboveLeft:
                aboveLeft.SetActive(true);
                Invoke("disableAttacker_aboveLeft", 0.2f);
                break;
            case AttackDirection.aboveFlipLeft:
                aboveFlipLeft.SetActive(true);
                Invoke("disableAttacker_aboveFlipLeft", 0.2f);
                break;
        }
        //attacker.enabled = true;
        //attacker.offset = attackerOffset;
    }

    public void disableAttacker()
    {

        switch (attackDirection)
        {
            case AttackDirection.aboveFlipRight:
                aboveFlipRight.SetActive(false);
                break;
            case AttackDirection.aboveFlipLeft:
                aboveFlipLeft.SetActive(false);
                break;
            case AttackDirection.right:
                right.SetActive(false);
                break;
            case AttackDirection.aboveRight:
                aboveRight.SetActive(false);
                break;
            case AttackDirection.belowRight:
                belowRight.SetActive(false);
                break;
            case AttackDirection.left:
                left.SetActive(false);
                break;
            case AttackDirection.aboveLeft:
                aboveLeft.SetActive(false);
                break;
            case AttackDirection.belowLeft:
                belowLeft.SetActive(false);
                break;
            case AttackDirection.belowFlipRight:
                belowFlipRight.SetActive(false);
                break;
            case AttackDirection.belowFlipLeft:
                belowFlipLeft.SetActive(false);
                break;
        }
    }

    void disableAttacker_aboveFlipRight()
    {
        aboveFlipRight.SetActive(false);
    }
    void disableAttacker_aboveRight()
    {
        aboveRight.SetActive(false);
    }
    void disableAttacker_right()
    {
        right.SetActive(false);
    }
    void disableAttacker_belowRight()
    {
        belowRight.SetActive(false);
    }
    void disableAttacker_belowFlipRight()
    {
        belowFlipRight.SetActive(false);
    }
    void disableAttacker_belowFlipLeft()
    {
        belowFlipLeft.SetActive(false);
    }
    void disableAttacker_belowLeft()
    {
        belowLeft.SetActive(false);
    }
    void disableAttacker_left()
    {
        left.SetActive(false);
    }
    void disableAttacker_aboveLeft()
    {
        aboveLeft.SetActive(false);
    }
    void disableAttacker_aboveFlipLeft()
    {
        aboveFlipLeft.SetActive(false);
    }

    public enum AttackDirection
    {
        aboveFlipRight,
        aboveFlipLeft,
        right,
        aboveRight,
        belowRight,
        left,
        aboveLeft,
        belowLeft,
        belowFlipRight,
        belowFlipLeft

    }
}
