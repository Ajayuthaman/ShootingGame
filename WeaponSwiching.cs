using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwiching : MonoBehaviour
{
    public GameObject rifle;
    public GameObject sniper;

    public GameObject rifleUI;
    public GameObject sniperUI;

    public GameObject pickupRifle;
    public GameObject pickupSniper;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !pickupRifle.activeInHierarchy)
        {
            sniper.SetActive(false);
            sniperUI.SetActive(false);

            rifle.SetActive(true);
            rifleUI.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !pickupSniper.activeInHierarchy)
        {
            rifle.SetActive(false);
            rifleUI.SetActive(false);

            sniper.SetActive(true);
            sniperUI.SetActive(true);
        }
    }
}
