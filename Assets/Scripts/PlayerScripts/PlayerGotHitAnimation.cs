using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGotHitAnimation : MonoBehaviour
{

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    bool goToOneAlpha = false;
    int blinkTime = 0, blinkLimit = 4;
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderSpritesDefault = myRenderer.material.shader;
        shaderGUItext = Shader.Find("GUI/Text Shader");

    }
    public void goBlink()
    {
        blinkTime = 0;
        blinkLimit = 4;
        whiteSprite();
    }
    public void goBlink(int blinkLimitParam)
    {
        blinkTime = 0;
        blinkLimit = blinkLimitParam;
        whiteSprite();
    }
    public void whiteSprite()
    {
        if (blinkTime < blinkLimit)
        {
            myRenderer.material.shader = shaderGUItext;
            var color = Color.white;
            if (myRenderer.color.a == 1 || !gameObject.CompareTag("tree"))
            {
                goToOneAlpha = true;
            }
            color.a = myRenderer.color.a - 0.1f;
            myRenderer.color = color;
            if (blinkTime == blinkLimit - 1)
            {
                Invoke("backToNormalAndUnInvisibled", 0.08f);
            }
            else
            {
                Invoke("normalSprite", 0.08f);
            }
            
        }
        
    }
    public void normalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        var color = Color.white;
        if (goToOneAlpha)
        {
            color.a = 1;
        }
        else
        {
            color.a = myRenderer.color.a;
        }
        myRenderer.color = color;
        blinkTime++;
        Invoke("whiteSprite", 0.08f);
    }
    public void backToNormalAndUnInvisibled()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        var color = Color.white;
        if (goToOneAlpha)
        {
            color.a = 1;
        }
        else
        {
            color.a = myRenderer.color.a;
        }
        myRenderer.color = color;
        blinkTime++;
        FindObjectOfType<GameManager>().playerMovementBridge.playerInvisibled = false;
    }
}
