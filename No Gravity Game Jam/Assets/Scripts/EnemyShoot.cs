using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Animator anim;
    
    public GameObject FireBall;
    public Transform Mouth;
    private bool CoolDown;

    void Awake()
    {
        CoolDown= false;
    }
    private void Update()
    {
        
        if (!CoolDown)
        {
            StartCoroutine(ShootCoolDown());
        }
    }
    IEnumerator ShootCoolDown()
    {
        Shoot();
        CoolDown= true;
        yield return new WaitForSeconds(5);
        CoolDown= false;

    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");
        GameObject go = Instantiate(FireBall, Mouth.position, Quaternion.identity);

        Vector3 dir = new Vector3(transform.localScale.x, 0);

        go.GetComponent<Projectile>().Setup(dir);
    }
}
