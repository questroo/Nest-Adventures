using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackManager : MonoBehaviour
{
    public CharacterManager characterManager;
    PlayerControls attackControls;

    public GameObject meleeOrb;
    private PlayerController playerController;
    private ProjectileController projectileController;
    public float damage = 50.0f;
    public bool canDealDamage = true;

    private void Awake()
    {
        attackControls = new PlayerControls();

        //attackControls.ActionMap.Attack.performed += ctx => Attack();
        attackControls.ActionMap.Attack.performed += (x) => Attack();
    }
    private void Start()
    {
        projectileController = GetComponent<ProjectileController>();
        playerController = GetComponent<PlayerController>();
    }

    void Attack()
    {
        if (characterManager.GetCurrentPlayerTag() == "Tanjiro")
        {
            StartCoroutine("MeleeAttack");
        }
        else
        {
            projectileController.ShootProjectile();
        }
    }

    IEnumerator MeleeAttack()
    {
        Vector3 spawnPosition = transform.position + (transform.forward * 1.0f);
        spawnPosition.y += 1.0f;
        GameObject spawnedOrb = Instantiate(meleeOrb, spawnPosition, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(spawnedOrb);
    }
    private void OnEnable()
    {
        attackControls.ActionMap.Enable();
    }

    private void OnDisable()
    {
        attackControls.ActionMap.Disable();
    }
}
