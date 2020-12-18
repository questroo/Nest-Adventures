using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PugilistPlayerController : MonoBehaviour
{
    public float comboCooldownTime = 2.0f;
    private float timeRemaining = 0.0f;

    Animator pugilistAnimator;
    public int attackCount = 0;
    public int lastAttackCount = 0;
    public bool isPugilistAttacking = false;

    public ParticleSystem punchParticleDust;
    public ParticleSystem punchParticleExplosion;
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
        punchParticleDust.Emit(1);
        punchParticleExplosion.Emit(1);
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
    }

    public void AlertEndOfSecondPunch()
    {
        pugilistAnimator.SetBool("PunchTwo", false);
    }

    public void AlertEndOfPunchCombo()
    {
        pugilistAnimator.SetBool("PunchCombo", false);
        attackCount = 0;
    }
}