//
//Created by Marco Garzon Lara on 15/03/2023
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable //The interact behaviour. This can be inherited by any object to contain customizeable interact behaviour. Import this interface into any script and define the behaviour of 'interact()'
{
    public void Interact();
}

public class Interactor : MonoBehaviour //The object that goes an interacts with interactable
{
    public Transform InteractorSource; //A reference to a transform from where a ray will be casted. If added to player, then the source transform needs to be the player camera.
    public float InteractRange; //length of interact raycast (in meters)

    // Start is called before the first frame update
    void Start()
    {
        //this can be empty
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 debugRayRange = InteractorSource.forward * InteractRange; //doing this to get visual feedback for ray range when debugging
        Debug.DrawRay(InteractorSource.position, InteractorSource.forward, Color.green); //draw the ray for debug purposes

        if(Input.GetKeyDown(KeyCode.E)) //if 'E' is pressed on an interaction-enabled object
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) //If an object is being pointed at and does in fact contain interactable component
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
