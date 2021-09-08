using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimActionModeSwitchBtn : MonoBehaviour
{
    float step = 0.05f;
    RectTransform rect;
    float xStep, xStart, StartScale = 0.5f, target;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        target = rect.localScale.x < 0 ? -1 : 1;
        xStart = rect.localScale.x < 0 ? -StartScale : StartScale;
        rect.localScale = new Vector3(xStart, StartScale, StartScale);
    }
    void FixedUpdate()
    {
        var updateCondition = target < 0 ? rect.localScale.x > target : rect.localScale.x < target;
        if (updateCondition)
        {
            xStep = rect.localScale.x < 0 ? -step : step;
            rect.localScale += new Vector3(xStep, step, step);
        }
        
    }
    private void OnDisable()
    {
        rect.localScale = new Vector3(xStart, StartScale, StartScale);
    }

}
