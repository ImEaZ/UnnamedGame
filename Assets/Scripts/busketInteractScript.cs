using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busketInteractScript : MonoBehaviour
{
    GameObject eBtn;
    bool MobileTest = false;
    private void Start()
    {
        MobileTest = FindObjectOfType<GameManager>().MobileTest;
        if (Application.platform == RuntimePlatform.Android || MobileTest)
        {

        }
        else
        {
            eBtn = gameObject.transform.Find("EButton").gameObject;
            eBtn.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android || MobileTest)
        {

        }
        else
        {
            eBtn.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
            if (Input.GetKey("e"))
            {
                FindObjectOfType<GameManager>().BusketPanelToggle();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Application.platform == RuntimePlatform.Android || MobileTest)
            {

            }
            else
            {
                eBtn.SetActive(true);
                FindObjectOfType<GameManager>().allowToOpenBusket = true;
                FindObjectOfType<GameManager>().busketItemTemp = gameObject.GetComponent<busketItems>().itemList;
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Application.platform == RuntimePlatform.Android || MobileTest)
            {

            }
            else
            {
                eBtn.SetActive(true);
                FindObjectOfType<GameManager>().allowToOpenBusket = true;
                FindObjectOfType<GameManager>().busketItemTemp = gameObject.GetComponent<busketItems>().itemList;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Application.platform == RuntimePlatform.Android || MobileTest)
            {

            }
            else
            {
                eBtn.SetActive(false);
                FindObjectOfType<GameManager>().allowToOpenBusket = false;
            }
        }
    }
}
