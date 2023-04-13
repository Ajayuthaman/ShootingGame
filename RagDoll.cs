using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagDoll : MonoBehaviour
{
    private Collider mainCollider;
    private Collider[] allCollider;
    private Rigidbody[] allRigidbody;
    private void Start()
    {
        mainCollider = GetComponent<Collider>();
        allCollider = GetComponentsInChildren<Collider>(true);
        allRigidbody = GetComponentsInChildren<Rigidbody>(true);
        KineRigidBody();
    }

    private void KineRigidBody()
    {
        foreach (var rigbody in allRigidbody)
        {
            rigbody.isKinematic = true;
        }
    }
    public void DoRagDoll(bool isRagDoll)
    {
        foreach (var col in allCollider)
        {
            col.enabled = isRagDoll;
        }
        foreach (var rigid in allRigidbody)
        {
            rigid.isKinematic = !isRagDoll;
        }
        mainCollider.enabled = !isRagDoll;
        GetComponent<Rigidbody>().useGravity = !isRagDoll;
        GetComponent<Animator>().enabled = !isRagDoll;
        GetComponent<Enemy>().enabled = !isRagDoll;
        GetComponent<NavMeshAgent>().enabled = !isRagDoll;
    }
}
