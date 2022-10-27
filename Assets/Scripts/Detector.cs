using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Detector : MonoBehaviour
{
    IInteractable interactComponent;
    Button interactButton;
    
    void Start()
    {
        interactButton = GameObject.Find("InteractButton").GetComponent<Button>();
    }

    void Update()
    {
  

        if (interactComponent!=null)
        {
            interactButton.interactable = true;
        }
        else { interactButton.interactable = false; }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Plane")
        {
            interactComponent = other.gameObject.GetComponent<IInteractable>();
        }      
    }
    private void OnTriggerExit(Collider other)
    {
            interactComponent = null;
    }

    public void InteractButton()
    {
        if (interactComponent != null)
        {
            interactComponent.Interact();
        }
    }

}
