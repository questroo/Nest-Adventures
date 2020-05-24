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

    // Combo stuff
    //Cooldown time between attacks (in seconds)
    public float cooldown = 0.5f;
    //Max time before combo ends (in seconds)
    public float maxTime = 1.0f;
    //Cooldown between combos
    public float comboCooldown = 2.0f;
    //Max number of attacks in combo
    public int maxCombo = 3;
    //Current combo
    int combo = 0;
    //Time of last attack
    float lastTime;

    private void Awake()
    {
        attackControls = new PlayerControls();

        attackControls.ActionMap.Attack.performed += (x) => Attack();
    }
    private void Start()
    {
        projectileController = GetComponent<ProjectileController>();
        playerController = GetComponent<PlayerController>();
        StartCoroutine("ComboAttack");
    }

    void Attack()
    {
        if (!disableInput)
        {

        }
    }
    public IEnumerator MeleeAttack(int comboNumber)
    {
        if (characterManager.GetCurrentPlayerTag() == "Tanjiro")
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
        else
        {
            projectileController.ShootProjectile(comboNumber);
        }
    }

    IEnumerator ComboAttack()
    {
        //Constantly loops so you only have to call it once
        while (true)
        {
            //Checks if attacking and then starts off the combo
            if (attackControls.ActionMap.Attack.triggered)
            {
                combo++;
                if (!disableInput)
                {
                    GetComponent<AttackManager>().StartCoroutine("MeleeAttack", combo);
                }
                Debug.Log("Attack" + combo);
                lastTime = Time.time;

                //Combo loop that ends the combo if you reach the maxTime between attacks, or reach the end of the combo
                while ((Time.time - lastTime) < maxTime && combo < maxCombo)
                {
                    //Attacks if your cooldown has reset
                    if (attackControls.ActionMap.Attack.triggered && (Time.time - lastTime) > cooldown)
                    {
                        combo++;
                        if (!disableInput)
                        {
                            StartCoroutine("MeleeAttack", combo);
                        }
                        Debug.Log("Attack " + combo);
                        lastTime = Time.time;
                    }
                    yield return null;
                }
                //Resets combo and waits the remaining amount of cooldown time before you can attack again to restart the combo
                combo = 0;
                yield return new WaitForSeconds(comboCooldown - (Time.time - lastTime));
            }
            yield return null;
        }
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
