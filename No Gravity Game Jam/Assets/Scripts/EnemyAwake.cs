using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwake : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int maxHealth = 1;
    public int currentHealth;
    public float Time;
    CircleCollider2D circleCollider;
    Animator anim;

    void Awake()
    {
        Time = Random.Range(1, 2);
        rb.velocity = transform.right * -speed;
        currentHealth = maxHealth;
        circleCollider= GetComponent<CircleCollider2D>();
        anim= GetComponent<Animator>();
        StartCoroutine(Countdown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TakeDamage(1);
            Debug.Log(gameObject.name);
        }
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth--;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            circleCollider.isTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            circleCollider.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (gameObject.transform.position.x < collision.transform.position.x)
            {
                anim.SetTrigger("pushed");
                Debug.Log("hi");
                rb.velocity = transform.right * -speed * 4;
            }

            else
            {
                return;
            }
        }
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(Time);
        rb.velocity = Vector3.zero;
        anim.SetBool("Arrived", true);
    }


}
