using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItem : MonoBehaviour
{
    public bool allowToCollect = false;
    GameObject parentObj;
    private void Start()
    {
        parentObj = gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerItemCollecter") && allowToCollect)
        {
            // Play sound & show pickup animation here
            //FindObjectOfType<GameManager>().PlayerCollectItem(parentObj.name);   *** Old inventory
            FindObjectOfType<GameManager>().addItemIntoPlayer(parentObj.name, null);
            Destroy(parentObj);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerItemCollecter") && allowToCollect)
        {
            // Play sound & show pickup animation here
            //FindObjectOfType<GameManager>().PlayerCollectItem(parentObj.name);   *** Old inventory
            FindObjectOfType<GameManager>().addItemIntoPlayer(parentObj.name, null);
            Destroy(parentObj);
        }
    }
}
