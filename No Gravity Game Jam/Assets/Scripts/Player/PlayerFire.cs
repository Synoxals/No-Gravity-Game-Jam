using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool Cooldown;
    Animator anim;
    public float timerMax;
    public float shootTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shootTimer = timerMax;
    }
    void Update()
    {
        if(!Cooldown)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                
                StartCoroutine(Shoot());

            }
        }
        if (shootTimer <= 0f) 
        {
            anim.SetBool("Shooting", false);
        }
        shootTimer = shootTimer - 0.005f;
    }

    private IEnumerator Shoot()
    {
        Cooldown = true;
        shootTimer = timerMax;
        anim.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.1f);
        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Cooldown= false;
    }
}
