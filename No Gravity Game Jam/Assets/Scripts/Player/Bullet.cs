using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Enemy hit");
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

}
