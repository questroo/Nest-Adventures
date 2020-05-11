using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileController : MonoBehaviour
{
    public PlayerControls controller;
    public CharacterManager characterManager;
    public CameraController cameraController;

    public GameObject projectile;
    public Transform projectileSpawnLocation;
    public float projectileForce = 2.0f;
    public float projectileLifeTime = 1.0f;
    private void Awake()
    {
        controller = new PlayerControls();

        //controller.ActionMap.Attack.performed += ctx => ShootProjectile();
        characterManager = FindObjectOfType<CharacterManager>();
    }

    public void ShootProjectile()
    {
        if (characterManager.GetCurrentPlayerTag() == "Bertha")
        {
            Rigidbody projRb = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity).GetComponent<Rigidbody>();

            if (cameraController.GetLockOn())
            {
                Debug.Log("locked on");
                Transform target = cameraController.GetCurrentlyLockedOnTransform();
                if(target)
                {
                    projRb.AddForce(Vector3.Normalize(target.position - projRb.transform.position) * projectileForce);
                    StartCoroutine("DestroyProjectile", projRb.gameObject);
                }
            }
            else
            {
                Debug.Log("not locked on");
                projRb.AddForce(transform.forward * projectileForce);
                StartCoroutine("DestroyProjectile", projRb.gameObject);
            }
        }
    }
    IEnumerator DestroyProjectile(GameObject proj)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(proj);
    }
}