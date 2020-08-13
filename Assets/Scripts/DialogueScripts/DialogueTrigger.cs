using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isPlayerNear = false;
    public bool hasStarted = false;

    DialogueManager dialogueManagerScript;

    private void Update()
    {
        if (isPlayerNear && !hasStarted && Input.GetKeyDown(KeyCode.J))
        {
            hasStarted = true;
            Trigger();
        }
        else if (isPlayerNear && hasStarted && Input.GetKeyDown(KeyCode.J))
        {
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
            dialogueManagerScript.EndDialogue();
        }
    }
}