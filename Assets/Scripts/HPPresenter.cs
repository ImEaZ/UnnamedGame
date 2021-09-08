using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPresenter : MonoBehaviour
{
    public int HPNumber = 1;
    Image img;
    private void Start()
    {
        img = GetComponent<Image>();
    }
    void FixedUpdate()
    {
        var hpCurrent = FindObjectOfType<GameManager>().player.hpCurrent;
        if (HPNumber <= hpCurrent)
        {
            img.color = new Color(1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(0.25f, 0.25f, 0.25f);
        }
    }
}
