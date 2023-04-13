using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [Header("Riffle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float FireCharge = 15f;
    public float nextTimeShoot = 0f;
    public Animator animator;
    public PlayerScript player;
    public Transform hand;
    public GameObject scopeUI;
    public GameObject scopeCam;

    [Header("Rifle Ammunation and Shooting")]
    private int maximumAmmunation = 5;
    public int mag = 2;
    private int presentAmmunation;
    public float reloadingTime = 2f;
    private bool setReloading = false;

    [Header("Rifle Effect")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodenEffect;
    public GameObject bloodEffect;

    [Header("Sounds & UI")]
    [SerializeField] private GameObject AmmoOutUI;
    [SerializeField] private int timeToShowUI = 1;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;  
    public AudioSource audioSource;


    private void Awake()
    {
        transform.SetParent(hand);
        presentAmmunation = maximumAmmunation;
    }

    private void Update()
    {
        if (setReloading)
            return;

        if(presentAmmunation <= 0 && mag>0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire2"))
        {
            //scopeCam.SetActive(true);
            scopeUI.SetActive(true);
        }
        else
        {
            //scopeCam.SetActive(false);
            scopeUI.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeShoot = Time.time + 1f/ FireCharge;
            Shoot();
        }
        else if(Input.GetButtonDown("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("FireWalk", true);
        }
        else if(Input.GetButton("Fire2") && Input.GetButtonDown("Fire1"))
        {            

            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
        }

        if (!Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Fire", false);
        }
        
        //ui update
        AmmoCount.occurance.UpdateAmmoText(presentAmmunation);
        AmmoCount.occurance.UpdateMagText(mag);

    }

    private void Shoot()
    {
        //check magazine

        if(mag == 0 && presentAmmunation ==0)
        {
            //show no amo
            StartCoroutine(ShowAmmoOut());

            return;
        }

        presentAmmunation--;

        if (mag > 0)
        {
            if (presentAmmunation == 0)
            {
                mag--;
            }
        }
       /* if(presentAmmunation== 0)
        {
            mag--;
        }*/
        

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectDistroy distroy = hitInfo.transform.GetComponent<ObjectDistroy>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

            if(distroy != null)
            {
                distroy.objectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if(enemy != null)
            {
                enemy.EnemyHitDamage(giveDamageOf);
                GameObject BloodGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(BloodGo, 2f);
            }
        }
    }

    IEnumerator Reload()
    {
        scopeUI.SetActive(false);
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;

        Debug.Log("Reloading..");
        player.animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound); 

        yield return new WaitForSeconds(reloadingTime);

        player.animator.SetBool("Reloading", false);
        presentAmmunation = maximumAmmunation;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        setReloading = false;
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        AmmoOutUI.SetActive(false);
    }
}
