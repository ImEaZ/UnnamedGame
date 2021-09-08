using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinSectionScript : MonoBehaviour
{
    public GameObject halfBtn, allBtn;
    public void showDropBtn()
    {
        halfBtn.SetActive(true);
        allBtn.SetActive(true);
    }

    public void hideDropBtn()
    {
        halfBtn.SetActive(false);
        allBtn.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && FindObjectOfType<GameManager>().mouseOnHalfDropBtn == false && FindObjectOfType<GameManager>().mouseOnAllDropBtn == false)
        {
            hideDropBtn();
        }
    }
}
