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
        dialogueManagerScript = FindObjectOfType<DialogueManager>();
        dialogueManagerScript.StartDialouge(dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Tanjiro") || other.CompareTag("Bertha"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Tanjiro") || other.CompareTag("Bertha"))
        {
            isPlayerNear = false;
            hasStarted = false;
        }
    }
}
