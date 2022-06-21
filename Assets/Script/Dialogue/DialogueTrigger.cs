using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue_ru;
    public Dialogue dialogue_eng;

    private void Start()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue_ru);
        } else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue_eng);
        }
    }
}