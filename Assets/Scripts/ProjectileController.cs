using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileController : MonoBehaviour
{
    public PlayerControls controller;
    public CharacterManager characterManager;

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

            projRb.AddForce(transform.forward * projectileForce);
            StartCoroutine("DestroyProjectile", projRb.gameObject);
        }
    }
    IEnumerator DestroyProjectile(GameObject proj)
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(proj);
    }
}