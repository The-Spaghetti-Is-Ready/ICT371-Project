//
// Created by Marco Garzon Lara on the 15/03/2024
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractDebugNum : MonoBehaviour, IInteractable //debug class that prints when an object is interacted with. Implementes the 'Interactable' interface
{
    // Start is called before the first frame update
    public void Interact() //funcion that is called when the interactable trigger is hit but this one has a random number generator
    {
        Debug.Log("Your random interactable number is: " + Random.Range(0, 100));
    }
}
