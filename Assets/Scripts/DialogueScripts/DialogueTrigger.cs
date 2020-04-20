using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Trigger()
    {
        FindObjectOfType<DialogueManager>().StartDialouge(dialogue);
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == ("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Trigger();

                // I'm stuck here.....   :<
            }
        }
    }


}
