using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SorcererPlayerController : MonoBehaviour
{
    [Header("Animation Variables")]
    Animator sorcererAnimator;
    public bool isAttacking = false;

    [Header("Attack Variables")]
    public float iceBallDamage = 20.0f;
    public GameObject iceBall;
    public Transform launchPoint;
    Transform target;

    private void Awake()
    {
        ServiceLocator.Register<SorcererPlayerController>(this);
    }

    private void Start()
    {
        sorcererAnimator = GetComponentInChildren<Animator>();
    }

    public void SendAttack(Transform _target)
    {
        target = _target;
        sorcererAnimator.SetBool("IsAttacking", true);
    }

    public void CancelAttack()
    {
        sorcererAnimator.SetBool("IsAttacking", false);
    }

    public bool IsSorcererAttacking()
    {
        return isAttacking;
    }

    public void AlertEndOfIceBall()
    {
        sorcererAnimator.SetBool("IsAttacking", false);
    }

    public void AlertLaunchIceBall()
    {
        GameObject ball = Instantiate(iceBall, launchPoint.position, transform.parent.rotation);
    }
}