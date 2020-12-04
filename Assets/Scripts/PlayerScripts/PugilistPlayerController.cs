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
    public float punchOneDamage = 5.0f;
    public float punchTwoDamage = 10.0f;
    public float punchComboDamage = 30.0f;
    public float attackRadius = 0.8f;
    public float attackDistance = 1.0f;
    public LayerMask enemyHitLayerMask;

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
        isPugilistAttacking = false;
        attackCount = 0;
        pugilistAnimator.SetBool("PunchOne", false);
        pugilistAnimator.SetBool("PunchTwo", false);
        pugilistAnimator.SetBool("PunchCombo", false);
    }

    public void AlertEndOfFirstPunch()
    {
        pugilistAnimator.SetBool("PunchOne", false);
        LaunchAttack(punchOneDamage);
    }

    public void AlertEndOfSecondPunch()
    {
        pugilistAnimator.SetBool("PunchTwo", false);
        LaunchAttack(punchTwoDamage);
    }

    public void AlertEndOfPunchCombo()
    {
        pugilistAnimator.SetBool("PunchCombo", false);
        LaunchAttack(punchComboDamage);
        attackCount = 0;
    }

    void LaunchAttack(float damage)
    {
        Debug.Log("Launching SphereCast");
        // Launch a spherecast (ALL HITS) starting at the position (located at the bottom of the character, hence + Vector3.up) with a radius and direction (in front of player, transform.forward) and a distance.
        // Only return hits with things on the Enemy layer (or whatever layer you choose)
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position + Vector3.up, attackRadius, transform.forward, attackDistance, enemyHitLayerMask);
        Debug.Log("recieved hitcount: " + raycastHits.Length);
        foreach(RaycastHit hit in raycastHits)
        {
            // Should only return enemy colliders
            EnemyStat stat = hit.collider.GetComponent<EnemyStat>();
            if(stat == null)
            {
                Debug.LogError("Enemy stat script not found for object " + stat.name + ". Unable to do damage to this 'Enemy'");
            }
            else
            {
                stat.TakeDamage(damage);
            }
        }
    }
}