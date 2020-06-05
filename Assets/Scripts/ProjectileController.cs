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
    public float rangedAttackStartupDelay = 1.0f;
    bool attackWindingUp = false;
    private void Awake()
    {
        //controller = new PlayerControls();

        //controller.ActionMap.Attack.performed += ctx => ShootProjectile();
        characterManager = FindObjectOfType<CharacterManager>();
    }

    public bool IsWindingUp()
    {
        return attackWindingUp;
    }
    IEnumerator ShootProjectile(int comboNumber)
    {
        attackWindingUp = true;
        yield return new WaitForSeconds(rangedAttackStartupDelay);
        Rigidbody projRb = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity).GetComponent<Rigidbody>();
        switch (comboNumber)
        {
            case 1:
                projRb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                projRb.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 3:
                projRb.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            default:
                break;
        }
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
            StartCoroutine("DestroyProjectile", projRb.gameObject);
        }
        attackWindingUp = false;
    }
    IEnumerator DestroyProjectile(GameObject proj)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(proj);
    }
}