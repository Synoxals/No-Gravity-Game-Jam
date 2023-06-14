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
<<<<<<< Updated upstream
        if(!Cooldown)
=======
        if (Input.GetKeyDown(KeyCode.V) && !shockwaveCooldown)
        {
            startShockwave = true;
            newShockwave = Instantiate(shockwavePrefab, shockwavePoint.position, shockwavePoint.rotation, shockwavePoint);
            StartCoroutine(ShockwaveCooldown());
        }

        if (!Cooldown)
>>>>>>> Stashed changes
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

    private void FixedUpdate()
    {
        if (startShockwave)
        {
            newShockwave.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
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
