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
        if (isPlayerNear && !hasStarted && Input.GetKeyDown(KeyCode.Space))
        {
            hasStarted = true;
            Trigger();
        }
        else if (isPlayerNear && hasStarted && Input.GetKeyDown(KeyCode.Space))
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
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hasStarted = false;
        }
    }
}
