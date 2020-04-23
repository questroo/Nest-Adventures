using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : MonoBehaviour
{
    public GameObject arrowObject;
    public Transform[] arrowInstantiateTransforms;
    public float arrowLaunchForce;
    public bool continuousFiring = false;

    bool isTriggered = false;
    GameObject arrowHandle;

    // Update is called once per frame
    void Update()
    {
        if(isTriggered)
        {
            isTriggered = false;
            FireArrows();
        }
    }

    public void TriggerTrap()
    {
        isTriggered = true;
    }

    private void FireArrows()
    {
        foreach(Transform arrowLaunchTransform in arrowInstantiateTransforms)
        {
            arrowHandle = Instantiate(arrowObject, arrowLaunchTransform.position, arrowLaunchTransform.rotation);

            Rigidbody arrowRigidBody = arrowHandle.GetComponent<Rigidbody>();

            arrowRigidBody.AddForce(arrowLaunchTransform.forward.normalized * arrowLaunchForce, ForceMode.Impulse);
        }
    }
}