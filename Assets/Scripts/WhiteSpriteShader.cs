using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSpriteShader : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    bool goToOneAlpha = false;
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderSpritesDefault = myRenderer.material.shader;
        shaderGUItext = Shader.Find("GUI/Text Shader");

    }
    public void whiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        var color = Color.white;
        if (myRenderer.color.a == 1 || !gameObject.CompareTag("tree"))
        {
            goToOneAlpha = true;
        }
        color.a = myRenderer.color.a - 0.1f;
        myRenderer.color = color;
        Invoke("normalSprite", 0.08f);
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
    }
}
