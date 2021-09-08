using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasDragAndDrop : MonoBehaviour
     , IPointerClickHandler
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
    , IPointerDownHandler
    , IPointerUpHandler
{
    Image sprite;
    Color target = Color.white;
    RectTransform rt;
    RectTransform boxRT;
    GameObject itemListParent, oldParentObj;
    void Awake()
    {
        itemListParent = transform.parent.transform.parent.transform.parent.gameObject;
        oldParentObj = transform.parent.gameObject;
        sprite = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        boxRT = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (sprite)
            sprite.color = Vector4.MoveTowards(sprite.color, target, Time.deltaTime * 10);

    }


    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        target = Color.gray;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent = oldParentObj.transform;
        transform.SetSiblingIndex(0);
        target = Color.white;
        rt.position = boxRT.position;
        var targetInd = FindObjectOfType<GameManager>().IVSwapTargetIndex;
        if (targetInd != -1)
        {
            //print($"Swaped S{srcInd} >< T{ind - 1}");
            if (targetInd == -999)
            {
                FindObjectOfType<GameManager>().showBinChoices();
            }
            else
            {
                FindObjectOfType<GameManager>().swapItemInList();
            }
        }
        else
        {
            FindObjectOfType<GameManager>().releaseIVSwapSourceItem();
            FindObjectOfType<GameManager>().releaseIVSwapTargetItem();
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        target = Color.gray;
        FindObjectOfType<GameManager>().draggingItem = true;
        rt.position = Input.mousePosition;
        var numbers = Regex.Split(transform.parent.gameObject.name, @"\D+").ToList();
        var checkNumber = numbers.Any(s => !string.IsNullOrWhiteSpace(s));
        if (checkNumber)
        {
            var ind = Convert.ToInt32(numbers.Where(s => !string.IsNullOrWhiteSpace(s)).FirstOrDefault());
            FindObjectOfType<GameManager>().registerIVSwapSourceItem(ind - 1);

        }
        transform.parent = itemListParent.transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //rt.position = boxRT.position;
    }
}
