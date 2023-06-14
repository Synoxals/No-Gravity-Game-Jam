using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool Cooldown;
    Animator anim;
    public float timerMax;
    public float shootTimer;
    public int Kills = 0;
    [SerializeField] TextMeshProUGUI KillCount;

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
        if (Kills >= 30) 
        {
            SceneManager.LoadScene("Win");
        }
        KillCount.text = "KILLS: " + Kills;
    }
    public void kill()
    {
        Kills++;
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
