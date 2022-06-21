using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class DialogueManager : MonoBehaviour {
    
    public Text dialogueText;
    public Text tapText;

    public GameObject dialoguePanel;
    public Animator personAnimator;

    public AudioSource audioSource;
    public AudioClip girlSound_1;
    public AudioClip girlSound_2;

    /*public Animator animator;*/

    private Queue<string> sentences;

    // Use this for initialization

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            SceneManager.LoadScene(1);
        }
    }

    void Start ()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            tapText.text = "Нажмите, чтобы продолжить...";
        } else
        {
            tapText.text = "Click to continue...";
        }
    }

    public void StartDialogue (Dialogue dialogue)
    {
        /*animator.SetBool("IsOpen", true);*/
        sentences = new Queue<string>();
        
        dialoguePanel.SetActive(true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
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

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        personAnimator.SetBool("isTalk", true);
        foreach (char letter in sentence.ToCharArray())
        {
            int rand = UnityEngine.Random.Range(0, 10);
            if (rand == 0)
            {
                audioSource.PlayOneShot(girlSound_1); 
            } else if (rand == 1)
            {
                audioSource.PlayOneShot(girlSound_2); 
            }
            
            yield return new WaitForSeconds(0.02f);
            dialogueText.text += letter;
            if (dialogueText.text.Length == sentence.Length)
            {
                personAnimator.SetBool("isTalk", false);
            }
        }
    }

    void EndDialogue()
    {
        /*animator.SetBool("IsOpen", false);*/
        SceneManager.LoadScene(1);
    }

}