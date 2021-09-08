using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class allDropBtnMouseCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<GameManager>().mouseOnAllDropBtn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<GameManager>().mouseOnAllDropBtn = false;
    }
}
