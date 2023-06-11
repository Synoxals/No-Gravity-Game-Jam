using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Debug.Log("Enemy hit");
            Destroy(other.gameObject);
            player.GetComponent<PlayerFire>().kill();
            Destroy(gameObject);
            Debug.Log("Bullet destroyed");
            
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
