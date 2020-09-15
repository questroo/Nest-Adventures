using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public CharacterManager characterManager;
    PlayerControls attackControls;

    private HitBoxProjection hitBoxProjection;
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

    public float meleeAttackWindup = 1.0f;
    public float meleeAttackRange = 1.0f;

    private void Awake()
    {
        attackControls = new PlayerControls();
    }
    private void Start()
    {
        hitBoxProjection = GetComponent<HitBoxProjection>();
        StartCoroutine("ComboAttack");
    }

    IEnumerator ComboAttack()
    {
        //Constantly loops so you only have to call it once
        while (true)
        {
            //Checks if attacking and then starts off the combo
            if (attackControls.ActionMap.Attack.triggered && (Time.time - lastTime) > cooldown)
            {
                //GetComponent<PlayerController>().DisableInput();
                GetComponent<PlayerController>().isAttacking = true;
                combo++;
                GetComponent<AnimationController>().TriggerAttackAnimation(combo);
                hitBoxProjection.DoAttack(combo);
                lastTime = Time.time;

                //Combo loop that ends the combo if you reach the maxTime between attacks, or reach the end of the combo
                while ((Time.time - lastTime) < maxTime && combo < maxCombo)
                {
                    //Attacks if your cooldown has reset
                    if (attackControls.ActionMap.Attack.triggered && (Time.time - lastTime) > cooldown)
                    {
                        combo++;
                        GetComponent<AnimationController>().TriggerAttackAnimation(combo);
                        hitBoxProjection.DoAttack(combo);
                        lastTime = Time.time;
                    }
                    yield return null;
                }
                combo = 0;
            }
            //GetComponent<PlayerController>().EnableInput();
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
