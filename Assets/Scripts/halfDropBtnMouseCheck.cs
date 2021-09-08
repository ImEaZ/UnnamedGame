using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class halfDropBtnMouseCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<GameManager>().mouseOnHalfDropBtn = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<GameManager>().mouseOnHalfDropBtn = false;
    }
}
