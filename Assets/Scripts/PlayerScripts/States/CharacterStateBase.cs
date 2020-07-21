using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBase : StateMachineBehaviour
{
    private PlayerController playerController;
    public PlayerController GetPlayerController(Animator animator)
    {
        if(playerController == null)
        {
            playerController = animator.GetComponentInParent<PlayerController>();
        }
        return playerController;
    }
}
