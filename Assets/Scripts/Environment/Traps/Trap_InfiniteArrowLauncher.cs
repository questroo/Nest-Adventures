using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_InfiniteArrowLauncher : MonoBehaviour
{
    public GameObject arrowObject;
    public float arrowDamage;
    public float fireCooldown;
    public float arrowLaunchForce;

    GameObject arrowHandle;
    bool isArrowReady = true;

    private void Update()
    {
        if(isArrowReady)
        {
            StartCoroutine(ContinuousFireTimer());
            arrowHandle = Instantiate(arrowObject, transform.position, transform.rotation);
            Rigidbody arrowRigidbody = arrowHandle.GetComponent<Rigidbody>();
            arrowRigidbody.AddForce(transform.forward.normalized * arrowLaunchForce, ForceMode.Impulse);
        }
    }

    IEnumerator ContinuousFireTimer()
    {
        isArrowReady = false;
        yield return new WaitForSeconds(fireCooldown);
        isArrowReady = true;
    }
}