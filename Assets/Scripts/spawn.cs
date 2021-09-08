using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public new Rigidbody2D rigidbody;
    float startY;
    int countGroundTouched = 0, goToSide;
    int[] side = { -1, 1 };
    bool runSpawnAlgorithm = true;
    void Start()
    {
        runSpawnAlgorithm = true;
        goToSide = side[Random.Range(0, side.Length)];
        rigidbody.gravityScale = Random.Range(1f, 2f);
        startY = transform.position.y;
        Vector2 temp = new Vector2((1f * goToSide), 2f);
        rigidbody.AddForce(temp * 2, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < startY - 0.2f && runSpawnAlgorithm)
        {
            if (countGroundTouched > 1)
            {
                gameObject.GetComponentInChildren<collectItem>().allowToCollect = true;
                gameObject.GetComponent<magneticMove>().allowMagnet = true;
                runSpawnAlgorithm = false;
                rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                Vector2 temp = new Vector2((1f * goToSide), 10f);
                rigidbody.AddForce(temp * Random.Range(1f, 1.2f), ForceMode2D.Impulse);

                rigidbody.mass = 9;
                rigidbody.gravityScale = 8;
            }
            countGroundTouched++;
        }
    }
}
