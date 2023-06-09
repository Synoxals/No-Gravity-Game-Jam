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

    private void Awake()
    {
        StartCoroutine(Wait());
    }
    private void Update()
    {
        if (!charge)
        {
            
            transform.Translate(dir * speed * Time.deltaTime);
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
        if (collision.tag == "enemy" || collision.tag == "Background")
        {
            return;
        }
        else
        {
            Debug.Log("touched " +collision.name);
            Destroy(gameObject);
        }

    }    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        charge= false;
    }
}
