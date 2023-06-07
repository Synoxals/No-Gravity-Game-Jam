using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;

    private void Start()
    {
        
       rb.velocity = transform.right * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BGBorder"))
        {
            transform.position = new Vector3(46.36f, 0f, 0f);
        }
        
    }
}
