using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorInteractor : MonoBehaviour
{
    public Image targetImage;
    float lowAlpha = 186f;
    public void FullAlpha()
    {
        targetImage.color = new Color(255f, 255f, 255f, 1f);
    }
    public void ReduceAlpha()
    {
        targetImage.color = new Color(255f, 255f, 255f, 0.75f);
    }
}
