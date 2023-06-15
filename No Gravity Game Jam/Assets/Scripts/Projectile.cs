using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    
    private Vector2 dir;
    private bool charge = true;
    public string targetTag;
    public bool canKillEnemy = false;
    AudioSource fireballEffect;

    public Rigidbody2D rb;
    private void Awake()
    {
        StartCoroutine(Wait());
        fireballEffect = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!charge)
        {
            
            rb.velocity = -transform.right * speed;
        }
        
    }

    public void Setup(Vector2 dir)
    {
        this.dir = dir;
        GetComponent<SpriteRenderer>().flipX= dir.x ==1 ? false : true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {
            Destroy(gameObject);
        }
        else if ((collision.tag == "enemy" && !canKillEnemy) || collision.tag == "Background")
        {
            return;
        }
        else if (collision.tag == "Shockwave")
        {
            charge = true;
            rb.velocity = -rb.velocity;
            canKillEnemy = true;
        }
        else if (collision.tag == "enemy" && canKillEnemy)
        {
            Debug.Log("touched " +collision.name);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        charge= false;
        fireballEffect.Play();

    }
}
