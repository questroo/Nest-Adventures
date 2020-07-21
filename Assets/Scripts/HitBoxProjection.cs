using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitBoxProjection : MonoBehaviour
{
    public CameraController cameraController;

    public GameObject projectile;
    public Transform projectileSpawnLocation;
    public float projectileForce = 2.0f;
    public float projectileLifeTime = 1.0f;

    public void ProjectHitbox()
    {
        Debug.Log("Projecting hitbox");
        GameObject go = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
        StartCoroutine(DestroyProjectile(go));
    }
    public void ShootProjectile()
    {
        Rigidbody projRb = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity).GetComponent<Rigidbody>();

        if (cameraController.GetLockOn())
        {
            Transform target = cameraController.GetCurrentlyLockedOnTransform();
            if (target)
            {
                projRb.AddForce(Vector3.Normalize(target.position - projRb.transform.position) * projectileForce);
                StartCoroutine("DestroyProjectile", projRb.gameObject);
            }
        }
        else
        {
            projRb.AddForce(transform.forward * projectileForce);
            Invoke("DestroyProjectile", projectileLifeTime);
        }
    }
    IEnumerator DestroyProjectile(GameObject hitbox)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        if(hitbox)
        {
            Destroy(hitbox);
        }
    }
}