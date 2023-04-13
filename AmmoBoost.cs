using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoost : MonoBehaviour
{
    [Header("Ammo Boost")]
    public Riffle rifle;
    public AmmoCount ammoUI;
    private int magToGive = 10;
    private float radious = 2.5f;

    [Header("Sound")]
    public AudioClip ammoBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;
    public GameObject pickupUI;

    private void Update()
    {
        if (Vector3.Distance(transform.position, rifle.transform.position) < radious)
        {
            pickupUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                rifle.mag = magToGive;
                ammoUI.magtext.text = "Magzines" + magToGive;

                //sound effect
                audioSource.PlayOneShot(ammoBoostSound);
                Object.Destroy(gameObject, 1.5f);
            }
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }
}
