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
    [SerializeField]
    private bool disableInput = false;

    // Combo attack variables
    public float firstAttackCooldown = 1.0f;
    public float cooldownToStartNextAttack = 0.5f;
    private int numberOfMaxAttacks = 3;
    private int numberOfAttacks = 0;
    private float lastAttackTimer;

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
        if (!disableInput)
        {
            //if (characterManager.GetCurrentPlayerTag() == "Tanjiro")
            //{
            //    numberOfAttacks++;
            //    StartCoroutine("MeleeAttack");
            //}
            //else
            //{
            //    projectileController.ShootProjectile();
            //}
        }
    }
    public IEnumerator MeleeAttack(int comboNumber)
    {
        Vector3 spawnPosition = transform.position + (transform.forward * 1.0f);
        spawnPosition.y += 1.0f;
        GameObject spawnedOrb = Instantiate(meleeOrb, spawnPosition, transform.rotation);
        switch (comboNumber)
        {
            case 1:
                spawnedOrb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case 2:
                spawnedOrb.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case 3:
                spawnedOrb.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            default:
                break;
        }
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
    public void DisableInput()
    {
        disableInput = true;
    }
    public void EnableInput()
    {
        disableInput = false;
    }

    public bool CheckDisableInputStatus()
    {
        return disableInput;
    }
}
