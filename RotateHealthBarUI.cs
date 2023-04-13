using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBarUI : MonoBehaviour
{
    public Transform maincamera;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + maincamera.forward);
    }
}
