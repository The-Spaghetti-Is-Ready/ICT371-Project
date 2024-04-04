using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Teleport : MonoBehaviour
{

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerGameObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Teleport")
        {
            playerGameObject.GetComponent<FirstPersonController>().disabled = true;
            Debug.Log("Enter");
            
            playerGameObject.GetComponent<CharacterController>().Move(new Vector3(11.18f, 0f, 0f));
            
            playerGameObject.GetComponent<FirstPersonController>().disabled = false;
        }
    }
    
}
