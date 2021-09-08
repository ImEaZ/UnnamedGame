using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBusketAnimation : MonoBehaviour
{
    int i = 0;
    float fallStepByFrame = 0.3f;
    void Awake()
    {
        Vector3 newPos = transform.position;
        newPos.y += 0.4f;
        transform.position = newPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (i < 5)
        {
            i++;
            Vector3 newPos = transform.position;
            newPos.y -= fallStepByFrame;
            transform.position = newPos;
        }
        else
        {
            GetComponent<EdgeCollider2D>().enabled = true;
        }
    }
}
