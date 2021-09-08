using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDrop : MonoBehaviour
{
    public SpriteRenderer playerSprite;
    public Transform playerTranform;
    public float droping = 0.6f, dropScale = 0.09f;
    SpriteRenderer thisSpriteObj;
    void Start()
    {
        thisSpriteObj = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 temp = new Vector3();
        temp = playerTranform.position + new Vector3(0, 0.4f);
        if (playerSprite.flipX)
        {
            temp.x += 0.4f;
        }
        else
        {
            temp.x -= 0.4f;
        }
        temp.y += droping;
        thisSpriteObj.sortingLayerName = playerSprite.sortingLayerName;
        thisSpriteObj.sortingOrder = playerSprite.sortingOrder + 1;
        gameObject.transform.position = temp;
        droping -= dropScale;
        if (droping < 0.2f)
        {
            droping = 0.6f;
        }
    }
}
