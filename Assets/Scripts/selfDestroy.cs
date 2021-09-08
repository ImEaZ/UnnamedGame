using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    float deadLine = 20f, blinkTime = 7f;
    bool blink = false, transparenting = false;
    int  blinkFrame = 6, f = 0;
    void Start()
    {
        Invoke("blinkBeforeDestroy", (deadLine - blinkTime < 0 ? 0 : deadLine - blinkTime));
    }

    void FixedUpdate()
    {
        if (blink)
        {
            if (transparenting)
            {
                f += 1;
                if (f == blinkFrame)
                {
                    var tranColor = Color.white;
                    tranColor.a = 0;
                    gameObject.GetComponent<SpriteRenderer>().color = tranColor;
                    transparenting = false;
                    f = 0;
                }
            }
            else
            {
                f += 1;
                if (f == blinkFrame)
                {
                    var tranColor = Color.white;
                    tranColor.a = 255;
                    gameObject.GetComponent<SpriteRenderer>().color = tranColor;
                    transparenting = true;
                    f = 0;
                }
            }
        }
    }
    void blinkBeforeDestroy()
    {
        blink = true;
        transparenting = true;
        Invoke("reduceBlinkFrame", blinkTime / 2);
        Invoke("selfDestroyFunction", blinkTime);

    }
    void reduceBlinkFrame()
    {
        blinkFrame = 3;
    }
    void selfDestroyFunction()
    {
        Destroy(gameObject);
    }
}
