using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private string requiredTag;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanInvoke(other.gameObject))
            onTriggerEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (CanInvoke(other.gameObject))
            onTriggerExit.Invoke();
    }

    private bool CanInvoke(GameObject other)
    {
        return string.IsNullOrEmpty(requiredTag) || other.CompareTag(requiredTag);
    }
}
