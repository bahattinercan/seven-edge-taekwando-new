using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    string[] lines = new string[] {"Kapıyla Etkileşime Girdin!, Kutuya Tıklayarak Devam Et","Bu son yazı"};
    public void Interact()
    {
        Debug.Log("Door Code is working...");
        Dialogue.instantiate.StartDialogue(lines);
    }
}
