using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SorcererPlayerController : MonoBehaviour
{
    Animator sorcererAnimator;
    int attackCount = 0;
    bool isAttacking = false;

    private void Awake()
    {
        ServiceLocator.Register<SorcererPlayerController>(this);
    }

    private void Start()
    {
        sorcererAnimator = GetComponentInChildren<Animator>();
    }

    public void SendAttack()
    {
        attackCount++;
    }

    public void CancelAttack()
    {
        attackCount = 0;
    }

    public bool IsSorcererAttacking()
    {
        return isAttacking;
    }
}