using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiflePickUp : MonoBehaviour
{
    [Header("Riffle's")]
    public GameObject playerRifle;
    public GameObject playerSniper;
    public GameObject pickupRifle;
    //public PlayerPunch playerPuch;
    public GameObject pickupUI;
    public GameObject RifleUI;
    public GameObject SniperUI;

    [Header("Rilfe Assign Things")]
    public PlayerScript player;
    private float radious = 2.5f;
   // private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;
    public Animator animator;
    


    private void Awake()
    {
        playerRifle.SetActive(false);

    }

    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < radious)
        {
            pickupUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && !playerSniper.activeInHierarchy)
            {
                playerSniper.SetActive(true);
                pickupRifle.SetActive(false);
                SniperUI.SetActive(true);

                //sound

            }
            else if (Input.GetKeyDown(KeyCode.F) && playerSniper.activeInHierarchy)
            {
                playerSniper.SetActive(false);
                SniperUI.SetActive(false);
                playerRifle.SetActive(true);
                pickupRifle.SetActive(false);
                RifleUI.SetActive(true);
            }
        }
        else
        {
            pickupUI.SetActive(false);
        }
    }
}
