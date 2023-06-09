using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Animator anim;
    
    public GameObject FireBall;
    public Transform Mouth;
    private bool CoolDown = true;

    void Awake()
    {
        Invoke("Enter", 4);
    }
    private void Update()
    {
        
        if (!CoolDown)
        {
            CoolDown = true;
            StartCoroutine(ShootCoolDown());
        }
    }
    IEnumerator ShootCoolDown()
    {
        Shoot();
        yield return new WaitForSeconds(5);
        CoolDown= false;

    }

    public void Shoot()
    {
        Debug.Log("Shoot triggered");
        anim.SetTrigger("Shoot");
        GameObject go = Instantiate(FireBall, Mouth.position, Quaternion.identity);

        Vector3 dir = new Vector3(transform.localScale.x, 0);

        go.GetComponent<Projectile>().Setup(dir);
    }
    private void Enter()
    {
        CoolDown = false;
    }
}
