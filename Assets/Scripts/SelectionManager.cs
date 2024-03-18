using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: use layering instead of tag comparison

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Camera targetCam; //defines which camera is allowed to select the object with selection manager
    [SerializeField] private string selectableTag;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;

    private Transform _selection;
    Renderer selectionRenderer;
    private Ray r;

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        r = new Ray(targetCam.transform.position, targetCam.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(r, out hit))
        {
            var selection = hit.transform;
            if(selection.CompareTag(selectableTag))
            {
                selectionRenderer = selection.GetComponent<Renderer>();
                if(selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }
            // else
            // {
            //     if(selectionRenderer != null)
            //     {
            //         selectionRenderer.material = defaultMaterial;
            //     }
            // }
        }
    }
}
