using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeShadowChanger : MonoBehaviour
{
    public Transform shadowTrn;

    private void FixedUpdate()
    {
        if (!gameObject.CompareTag("stump"))
            return;

        shadowTrn.position = new Vector3(gameObject.transform.position.x + 0.0126f, gameObject.transform.position.y + 0.0252f);
        shadowTrn.localScale = new Vector3(2.213413f, 2.689213f);

        gameObject.GetComponent<TreeShadowChanger>().enabled = false;
    }
}
