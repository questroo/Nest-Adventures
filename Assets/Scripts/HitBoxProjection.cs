using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitBoxProjection : MonoBehaviour
{
    public CameraController cameraController;
    public CharacterManager characterManager;

    public GameObject projectile;
    public Transform projectileSpawnLocation;
    public float projectileForce = 2.0f;
    public float projectileLifeTime = 0.3f;

    public void DoAttack()
    {
        if (characterManager.GetCurrentPlayerTag() == "MeleeCharacter")
        {
            ProjectHitbox();
        }
        else
        {
            ShootProjectile();
        }
    }

    public void ProjectHitbox()
    {
        Debug.Log("Projecting hitbox");
        GameObject go = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
        StartCoroutine(DestroyProjectile(go));
    }
    public void ShootProjectile()
    {
        GameObject go = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity) as GameObject;
        
        if (cameraController.GetLockOn())
        {
            Debug.Log("Camera locked on");
            Transform target = cameraController.GetCurrentlyLockedOnTransform();
            if (target)
            {
                go.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target.position - go.transform.position) * projectileForce);
                StartCoroutine("DestroyProjectile", go.gameObject);
            }
        }
        else
        {
            Debug.Log("Adding force");
            go.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);
            StartCoroutine("DestroyProjectile", go.gameObject);
        }
    }
    IEnumerator DestroyProjectile(GameObject hitbox)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        if (hitbox)
        {
            Destroy(hitbox);
        }
    }
}