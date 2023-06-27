using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    [Header("Bullet Stuff")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool Cooldown;
    Animator anim;
    public float timerMax;
    public float shootTimer;
    public ParticleSystem FireParticles;

    [Header("Kills Stuff")]
    public int Kills = 0;
    [SerializeField] TextMeshProUGUI KillCount;
    int previousKills = 0;
    public bool disablewinCondition;

    [Header("Shockwave Stuff")]
    public GameObject shockwavePrefab;
    public bool shockwaveCooldown = false;
    public bool startShockwave = false;
    public Transform shockwavePoint;
    public float shockwaveTimer = 1.5f;
    GameObject newShockwave;
    public int maxCharges = 3;
    public GameObject chargeBar;
    Slider chargeSlider;

    [Header("Audio")]
    public AudioSource blasterSound;
    public AudioSource shockwaveSound;


    private void Start()
    {
        anim = GetComponent<Animator>();
        shootTimer = timerMax;
        chargeSlider = chargeBar.GetComponent<Slider>();
        chargeSlider.maxValue = maxCharges;
    }
    void Update()
    {
        //Update Debugs
        Debug.Log("Previous Kills: " + previousKills);
        Debug.Log("Current Kills: " + Kills);
        Debug.Log("Charge Slider Value: " + chargeSlider.value);

        if (Input.GetKeyDown(KeyCode.V) && !shockwaveCooldown && chargeSlider.value > 0)
        {
            startShockwave = true;
            shockwaveSound.Play();
            newShockwave = Instantiate(shockwavePrefab, shockwavePoint.position, shockwavePoint.rotation,shockwavePoint);
            StartCoroutine(ShockwaveCooldown());
            chargeSlider.value--;
        }

        if (Kills == previousKills + 10 && chargeSlider.value < 3)
        {
            chargeSlider.value += 1;
            previousKills += 10;
        }

        if (!Cooldown)

        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                
                StartCoroutine(Shoot());
                blasterSound.Play();
                FireParticles.Play();

            }
        }
        if (shootTimer <= 0f) 
        {
            anim.SetBool("Shooting", false);
        }
        shootTimer = shootTimer - 0.005f;

        
        if (Kills >= 30 && !disablewinCondition) 
        {
            SceneManager.LoadScene("Win");
        }
        KillCount.text = "KILLS: " + Kills;
       
    }

    private void FixedUpdate()
    {
        if (startShockwave)
        {
            newShockwave.transform.localScale += new Vector3(0.15f, 0.15f, 0.15f);
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

    private IEnumerator ShockwaveCooldown()
    {
        shockwaveCooldown = true;
        yield return new WaitForSeconds(0.3f);
        startShockwave = false;
        Destroy(newShockwave);
        yield return new WaitForSeconds(shockwaveTimer);
        shockwaveCooldown = false;
    }
}
