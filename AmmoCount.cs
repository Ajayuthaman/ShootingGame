using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text ammunationText;
    public Text magtext;

    public static AmmoCount occurance;

    private void Awake()
    {
        occurance = this;
    }

    public void UpdateAmmoText(int presentAmmunation)
    {
        ammunationText.text = "Ammo: " + presentAmmunation; 
    }

    public void UpdateMagText(int mag)
    {
        magtext.text = "Magazines: "+mag;
    }
}
