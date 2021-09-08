using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvesting : MonoBehaviour
{
    Transform playerTrn;
    public int hitCount = 0;
    public int itemIndex = -1;
    public int areaCode = -1;
    void Start()
    {
        playerTrn = GameObject.Find("Player").transform;
    }

    //private void OnMouseOver()
    //{
    //    var temp = Vector2.Distance(playerTrn.position, transform.position);
    //    if (temp < 1.6)
    //    {
    //        FindObjectOfType<GameManager>().cancelAttack();
    //        PlayerMovement.getResource = this.tag;
    //        PlayerMovement.interacting = gameObject;
    //    }
    //}
    //private void OnMouseExit()
    //{
    //    PlayerMovement.getResource = "";
    //}
}
