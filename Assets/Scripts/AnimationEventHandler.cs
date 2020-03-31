using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    PlayerController playerController;
    // Start is called before the first frame update

    [SerializeField] float rollDuration = 2.0f;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnDodgeRoll()
    {
        playerController.TurnOffIsDodgingBool();
    }


    private void Update()
    {
        playerController.transform.position += playerController.transform.position - transform.position;
    }

}
