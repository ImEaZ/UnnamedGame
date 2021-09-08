using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushGotHit : MonoBehaviour
{
    public bool reNew = false;
    Vector3 originalScale, animationScale;
    bool bigger, smaller;
    public float originalMinScaleY, originalMaxScaleY, originalReScaleValue;
    float minScaleY, maxScaleY, reScaleValue;
    int i = 100;
    private void Start()
    {
        originalScale = gameObject.GetComponent<Transform>().localScale;
        animationScale = originalScale;
        bigger = true;
        smaller = false;
    }
    void reNewAnimation()
    {
        transform.localScale = originalScale;
        animationScale = originalScale;
        minScaleY = originalMinScaleY;
        maxScaleY = originalMaxScaleY;
        reScaleValue = originalReScaleValue;
        bigger = true;
        smaller = false;
        i = 0;
    }
    private void FixedUpdate()
    {
        if (reNew)
        {
            reNew = false;
            reNewAnimation();
        }
        if (i < 3)
        {
            gameObject.GetComponent<WhiteSpriteShader>().whiteSprite();
            if (bigger)
            {
                if (i == 0)
                {
                    animationScale.y = maxScaleY;
                    transform.localScale = animationScale;
                }
                animationScale.y += reScaleValue;
                transform.localScale = animationScale;
                if (transform.localScale.y > maxScaleY)
                {
                    bigger = false;
                    smaller = true;
                    maxScaleY -= 0.06f;
                }
            }
            else if (smaller)
            {
                animationScale.y -= reScaleValue;
                transform.localScale = animationScale;
                if (transform.localScale.y < minScaleY)
                {
                    ++i;
                    bigger = true;
                    smaller = false;
                    minScaleY += 0.06f;
                    reScaleValue -= 0.02f;
                }
            }
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}
