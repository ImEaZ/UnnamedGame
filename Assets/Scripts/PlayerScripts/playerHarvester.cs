using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHarvester : MonoBehaviour
{
    public static CircleCollider2D harvester;
    private void Start()
    {
        harvester = GetComponent<CircleCollider2D>();
    }
    private void FixedUpdate()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "stone":
            case "bush":
            case "berry":
            case "tree":
            case "stump":
                PlayerMovement.getResource = collision.tag;
                PlayerMovement.interacting = collision.gameObject;
                FindObjectOfType<GameManager>().SetActionImageButton(collision.tag);
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "stone":
            case "bush":
            case "berry":
            case "tree":
            case "stump":
                PlayerMovement.getResource = collision.tag;
                PlayerMovement.interacting = collision.gameObject;
                FindObjectOfType<GameManager>().SetActionImageButton(collision.tag);
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "stone":
            case "bush":
            case "berry":
            case "tree":
            case "stump":
                PlayerMovement.getResource = "";
                FindObjectOfType<GameManager>().SetActionImageButton("none");
                break;
        }
    }
}
