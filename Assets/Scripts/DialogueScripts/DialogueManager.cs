using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {

    }

    public void StartDialouge(Dialogue dialogue)
    {
        Debug.Log("Talking to " + dialogue.name);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    private void EndDialogue()
    {
        Debug.Log("End Dialogue");
    }

}
