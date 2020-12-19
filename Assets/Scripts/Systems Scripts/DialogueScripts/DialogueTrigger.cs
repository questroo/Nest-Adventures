using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isPlayerNear = false;
    public bool hasStarted = false;
    public bool isInputPressed = false;

    DialogueManager dialogueManagerScript;
    PlayerControls pc;

    private void Start()
    {
        pc = ServiceLocator.Get<PlayerControls>();
        pc.ActionMap.Attack.performed += ctx => KeyIsPressed();
    }

    void KeyIsPressed()
    {
        isInputPressed = true;
    }

    private void Update()
    {
        if (isPlayerNear && !hasStarted && isInputPressed)
        {
            isInputPressed = false;
            hasStarted = true;
            Trigger();
        }
        else if (isPlayerNear && hasStarted && isInputPressed)
        {
            isInputPressed = false;
            dialogueManagerScript.DisplayNextSentence();
        }
    }

    public void Trigger()
    {
        if(dialogueManagerScript == null)
            dialogueManagerScript = FindObjectOfType<DialogueManager>();

        dialogueManagerScript.StartDialouge(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("RangedCharacter") || other.CompareTag("MeleeCharacter"))
        {
            isPlayerNear = false;
            hasStarted = false;
            isInputPressed = false;
            dialogueManagerScript.EndDialogue();
        }
    }
}