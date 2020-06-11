using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Trap_Arrow : MonoBehaviour
{
    public GameObject arrowObject;
    public GameObject launcherObject;
    public List<Transform> arrowInstantiateTransforms;
    public float arrowLaunchForce;
    public bool continuousFiring = false;
    public float continuousFireingDelay = 1.0f;
    public bool arrowReady = true;

    bool isTriggered = false;
    GameObject arrowHandle;

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if(!continuousFiring)
                isTriggered = false;

            if(arrowReady)
                FireArrows();
        }
    }

    public void TriggerTrap()
    {
        isTriggered = true;
    }

    private void FireArrows()
    {
        StartCoroutine(ContinuousFireTimer());

        foreach (Transform arrowLaunchTransform in arrowInstantiateTransforms)
        {
            arrowHandle = Instantiate(arrowObject, arrowLaunchTransform.position, arrowLaunchTransform.rotation);

            Rigidbody arrowRigidBody = arrowHandle.GetComponent<Rigidbody>();

            arrowRigidBody.AddForce(arrowLaunchTransform.forward.normalized * arrowLaunchForce, ForceMode.Impulse);
        }
    }

    IEnumerator ContinuousFireTimer()
    {
        arrowReady = false;
        yield return new WaitForSeconds(continuousFireingDelay);
        arrowReady = true;
    }

    public void MakeNewArrowLauncher()
    {
        GameObject newLauncher = Instantiate(launcherObject, transform.position + Vector3.up, Quaternion.identity, transform);
        arrowInstantiateTransforms.Add(newLauncher.transform);
    }
}