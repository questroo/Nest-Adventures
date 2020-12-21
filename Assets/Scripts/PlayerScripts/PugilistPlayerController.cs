using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PugilistPlayerController : MonoBehaviour
{
    [Header("Animation Variables")]
    Animator pugilistAnimator;
    private float timeRemaining = 0.0f;

    public float comboCooldownTime = 2.0f;
    public int attackCount = 0;
    public int lastAttackCount = 0;
    public bool isPugilistAttacking = false;

    [Header("Attack Variables")]
    public float punchOneDamage = 10.0f;
    public float punchTwoDamage = 15.0f;
    public float punchComboDamage = 30.0f;
    public float attackRadius = 0.8f;
    public float attackDistance = 1.0f;
    public LayerMask enemyHitLayerMask;
    public Transform attackPosition;

    private void Awake()
    {
        ServiceLocator.Register<PugilistPlayerController>(this);
    }

    private void Start()
    {
        pugilistAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
            attackCount = 0;
    }

    public bool IsPugilistAttacking()
    {
        return isPugilistAttacking;
    }

    public void SendAttack()
    {
        isPugilistAttacking = true;
        attackCount++;
        timeRemaining = comboCooldownTime;

        if (attackCount == 1)
            pugilistAnimator.SetBool("PunchOne", true);
        else if (attackCount == 2)
            pugilistAnimator.SetBool("PunchTwo", true);
        else if (attackCount == 3)
            pugilistAnimator.SetBool("PunchCombo", true);
        else if (attackCount == 4)
            pugilistAnimator.SetBool("PunchOne", true);
        else
            attackCount = 4;
    }

    public void CancelAttack()
    {
        attackCount = 0;
        pugilistAnimator.SetBool("PunchOne", false);
        pugilistAnimator.SetBool("PunchTwo", false);
        pugilistAnimator.SetBool("PunchCombo", false);
        isPugilistAttacking = false;
    }

    public void AlertEndOfFirstPunch()
    {
        pugilistAnimator.SetBool("PunchOne", false);
        LaunchAttack(punchOneDamage);
        isPugilistAttacking = false;
    }

    public void AlertEndOfSecondPunch()
    {
        pugilistAnimator.SetBool("PunchTwo", false);
        LaunchAttack(punchTwoDamage);
        isPugilistAttacking = false;
    }

    public void AlertEndOfPunchCombo()
    {
        pugilistAnimator.SetBool("PunchCombo", false);
        LaunchAttack(punchComboDamage);
        attackCount = 0;
        isPugilistAttacking = false;
    }

    void LaunchAttack(float damage)
    {
        //Debug.Log("Launching SphereCast");

        Collider[] hitColliders = Physics.OverlapSphere(attackPosition.position, attackRadius);
        Debug.Log("Hit Collider Count: " + hitColliders.Length);
        if(hitColliders.Length > 0)
        {
            Debug.Log(hitColliders[0].gameObject.name);
        }

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy") || col.CompareTag("RangedEnemy"))
            {
                //col.GetComponent<EnemyStat>().TakeDamage(damage);
                col.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
    }
}