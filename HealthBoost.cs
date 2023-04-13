using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [Header("Health Boost")]
    public PlayerScript player;
    private float HealthToGive = 120f;
    private float radious = 2.5f;

    [Header("Sound")]
    public AudioClip HealthBoostSound;
    public AudioSource audioSource;

    [Header("HealthBox Animator")]
    public Animator animator;
    public GameObject pickupUI;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radious)
        {
            Debug.Log("near");
            pickupUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("Open", true);
                player.PresentHealth = HealthToGive;
                player.healthBar.SetHealth(HealthToGive);

                //sound effect
                audioSource.PlayOneShot(HealthBoostSound);
                Object.Destroy(gameObject, 1.5f);
            }
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }
}
