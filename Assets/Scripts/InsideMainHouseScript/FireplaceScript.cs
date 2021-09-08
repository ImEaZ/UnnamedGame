using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceScript : MonoBehaviour
{
    public SpriteRenderer fireplaceSprite;
    public GameObject woodObj;
    public SpriteRenderer wood;
    public GameObject Fire;

    public void LightFireplaceUp()
    {
        fireplaceSprite.sprite = Resources.Load<Sprite>("Fireplace/FireplaceWithFire");
    }

    public void LightFireplaceOff()
    {
        fireplaceSprite.sprite = Resources.Load<Sprite>("Fireplace/Fireplace");
    }
}
