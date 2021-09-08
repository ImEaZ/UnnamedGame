using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextDestroyer : MonoBehaviour
{
    public TextMeshPro textMP;
    bool startFadeOut = false;
    private void FixedUpdate()
    {
        if (startFadeOut)
        {
            var colorTMP = textMP.color;
            colorTMP.a -= 0.01f;
            textMP.color = colorTMP;
            if (colorTMP.a <= 0f)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        
    }

    public void StartFadeOut()
    {
        startFadeOut = true;
    }
}
