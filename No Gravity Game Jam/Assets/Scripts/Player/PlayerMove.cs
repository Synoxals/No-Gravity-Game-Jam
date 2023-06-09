using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float blastSpeed = 1f;
    public float beamSpeed = 3f;

    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float minxSpeed = -6f, minySpeed = -6f;
    public float maxxSpeed = 6f, maxySpeed = 6f;

    public Rigidbody2D rb;
    public Quaternion upRot, downRot, leftRot, rightRot;  
    private bool cooldown;
    private string facingDirection = "right";
    public int Health;
    Animator anim;

    public bool isDead = false;
    // Update is called once per frame

    private void Start()
    {
        anim= GetComponent<Animator>();
    }
    void Update()
    {

        
        // Handling inputs
        if (Input.GetKeyDown(KeyCode.D) && xSpeed > minxSpeed && !isDead)
        {
            xSpeed -= blastSpeed;
            facingDirection = "right";
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.A) && xSpeed < maxxSpeed && !isDead)
        {
            xSpeed += blastSpeed;
            facingDirection = "left";
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.W) && ySpeed > minySpeed && !isDead)
        {
            ySpeed -= blastSpeed;
            facingDirection = "up";
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.S) && ySpeed < maxySpeed && !isDead)
        {
            ySpeed += blastSpeed;
            facingDirection = "down";
            Flip();
        }
        
        if (Health<= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("Dead");
            Invoke("Die",2);
        }
    }

    private void FixedUpdate()
    {
        // Handling movement (In Fixed to avoid bugs)
        rb.velocity = new Vector2(xSpeed, ySpeed);
        xSpeed = xSpeed / 1.01f;
        ySpeed = ySpeed / 1.01f;
    }
    void Die()
    {
        SceneManager.LoadScene("Death");
    }

    private void Flip()
    {
        if(!cooldown) 
        {
            if (facingDirection == "left")
            {
                //Debug.Log("left triggered");
                leftRot.eulerAngles = new Vector3(0, 180, 0);
                StartCoroutine(Rotate(0, leftRot, 1000));
            }
            else if (facingDirection == "right")
            {
                //Debug.Log("right triggered");
                rightRot.eulerAngles = new Vector3(0, 0, 0);
                StartCoroutine(Rotate(0, rightRot, 1000));
            }

            else if (facingDirection == "up")
            {
                upRot.eulerAngles = new Vector3(0, 0, 90);

                StartCoroutine(Rotate(0, upRot, 1000));


            }
            else if (facingDirection == "down")
            {
                downRot.eulerAngles = new Vector3(0, 0, -90);

                StartCoroutine(Rotate(0, downRot, 1000));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        else
        {
            xSpeed = 0;
            ySpeed = 0;
        }
        if (collision.gameObject.CompareTag("ememyBullet") || collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Health--;
        }
    }

    IEnumerator Rotate(float seconds, Quaternion dir, int speed)
    {
        cooldown = true;
        var step = speed;

        while (transform.rotation != dir)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, step);
            yield return new WaitForSeconds(seconds);
        }
        cooldown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireBall"))
        {
            anim.SetTrigger("Dead");
        }
    }
}
