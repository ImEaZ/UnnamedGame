using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magneticMove : MonoBehaviour
{
    public bool allowMagnet = false;
    bool inside;
    Transform magnet;
    float radius = 30f;
    float force = 20f;
    Rigidbody2D rb;
    void Start()
    {
        magnet = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        inside = false;
    }
    private void FixedUpdate()
    {
        if (inside)
        {
            Vector3 magnetField = (magnet.position + new Vector3(0, 0.4f) - transform.position);
            float index = (radius - magnetField.magnitude) / radius;
            rb.gravityScale = 1;
            rb.mass = 1;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.AddForce(force * magnetField * index);
        }
        else
        {
            if (allowMagnet)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && allowMagnet)
        {
            inside = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && allowMagnet)
        {
            inside = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inside = false;
        }
    }
}
