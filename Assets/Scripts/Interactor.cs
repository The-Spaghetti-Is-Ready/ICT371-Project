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
   [SerializeField] private Material highlightMat; //The specified highlight material when a raycast hits an interactable

    private Transform currSelection; //current object being looked at

    public Transform InteractorSource; //A reference to a transform from where a ray will be casted. If added to player, then the source transform needs to be the player camera.
    public float InteractRange; //length of interact raycast (in meters)

    // Update is called once per frame
    void Update()
    {
        //Vector3 debugRayRange = InteractorSource.forward * InteractRange; //doing this to get visual feedback for ray range when debugging
        Debug.DrawRay(InteractorSource.position, InteractorSource.forward, Color.green); //draw the ray for debug purposes

        Ray r = new Ray(InteractorSource.position, InteractorSource.forward); //The ray being cast from object. TODO: optimize this so that we're not creating a new ray every frame, as it could maybe slow things down
        RaycastHit hit; //The raycast hit var denoting the object hit by player/entity ray

        if(Physics.Raycast(r, out hit, InteractRange))
        {
            if(hit.collider.gameObject.TryGetComponent(out IInteractable interactObj)) //If an object is being pointed at and does in fact contain interactable component
            {
                var selection = hit.transform; //grab the transform for the selected object (object that is pointed to by camera)
                var selectionRenderer = selection.GetComponent<Renderer>(); //Get object renderer component at the translate position of the selected object, then assign to new renderer
                if(selectionRenderer != null) //If the object being pointed to does in fact have a renderer and has an interactable component (interactable condition above)
                {
                    selectionRenderer.material = highlightMat; //change the material of selection to the highlight material
                }

                if(Input.GetKeyDown(KeyCode.E)) //if 'E' is pressed on an interaction-enabled object
                {
                    interactObj.Interact();
                }
                currSelection = selection;
            }
        }
    }
}
