using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public int maxHealth = 1;
    public int currentHealth;
    public float Timeer;
    CircleCollider2D circleCollider;
    Animator anim;
    public GameObject player;
    private bool rotating = false;
    public float rospeed;

    public float rotationModifier;

    private void FixedUpdate()
    {
        if (rotating)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }
    private void Awake()
    {

        if (transform.position.y > 0)
        {
            transform.Rotate(0, 0, 180);
        }
        rb.velocity = transform.up * 2;
        currentHealth = maxHealth;
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(Countdown());
    }
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Tab))
        {
            TakeDamage(1);
            Debug.Log(gameObject.name);
        }*/

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
        yield return new WaitForSeconds(Timeer);
        rb.velocity = Vector3.zero;
        StartCoroutine(LookAtPlayer());
        
    }
    IEnumerator LookAtPlayer()
    {
        rotating= true;
        yield return new WaitForSeconds(8);
        rotating= false;
        rb.velocity = transform.up * 4;
    }

}
