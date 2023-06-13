using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public Transform shockwavePoint;
    public GameObject bulletPrefab;
    public GameObject shockwavePrefab;
    private bool Cooldown;
    private bool startShockwave;
    private bool shockwaveCooldown = false;
    float targetscale = 10f;
    Animator anim;
    public float timerMax;
    GameObject newShockwave;
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
        if (Input.GetKeyDown(KeyCode.V) && !shockwaveCooldown)
        {
            startShockwave = true;
            newShockwave = Instantiate(shockwavePrefab, shockwavePoint.position, shockwavePoint.rotation, shockwavePoint);
            StartCoroutine(ShockwaveCooldown());
        }

        if (startShockwave)
        {
            newShockwave.transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }

        if (!Cooldown)
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
        Cooldown = false;
    }

    private IEnumerator ShockwaveCooldown()
    {
        shockwaveCooldown = true;
        yield return new WaitForSeconds(0.3f);
        startShockwave = false;
        Destroy(newShockwave);
        yield return new WaitForSeconds(5);
        shockwaveCooldown = false;
    }
}

