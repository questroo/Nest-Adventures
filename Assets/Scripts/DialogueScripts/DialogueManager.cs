using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    public float textSpeed = 0.1f;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialouge(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        animator.SetBool("isOpen", true);
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End Dialogue");

        animator.SetBool("isOpen", false);
    }
}
