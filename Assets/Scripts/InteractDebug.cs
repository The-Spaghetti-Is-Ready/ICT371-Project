//
// Created by Marco Garzon Lara on the 15/03/2024
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractDebug : MonoBehaviour, IInteractable //debug class that prints when an object is interacted with. Implementes the 'Interactable' interface
{
    // Start is called before the first frame update
    public void Interact() //funcion that is called when the interactable trigger is hit 
    {
        Debug.Log("Object has been interacted with");
    }
}
