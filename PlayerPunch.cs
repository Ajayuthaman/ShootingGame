using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
/*    [Header("Player Punch Var")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float punchingRange = 5f;

    [Header("Punch Effect")]
    public GameObject WoodenEffect;

    public void Punch()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectDistroy distroy = hitInfo.transform.GetComponent<ObjectDistroy>();

            if (distroy != null)
            {
                distroy.objectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
        }
    }*/
}
