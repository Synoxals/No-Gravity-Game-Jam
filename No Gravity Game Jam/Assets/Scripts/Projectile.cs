using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Animator PlayerAnim;
    private Vector2 dir;
    private bool charge;
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
        if (collision.tag== targetTag)
        {
            PlayerAnim.SetBool("Dead", true);
        }
        Destroy(this.gameObject);
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        charge= false;
    }
}
