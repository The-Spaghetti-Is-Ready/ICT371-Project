using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Teleport : MonoBehaviour
{
    // FirstPersonController playerController;
    // // Start is called before the first frame update
    //
    // void Awake()
    // {
    //     playerController = GameObject.Find("");
    // }
    //

    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject playerGameObject;
    // private FirstPersonController firstPersonController;
    void Start()
    {
        // firstPersonController = playerGameObject.GetComponent<FirstPersonController>();
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
            
            playerTransform.position.Set(11.18f, transform.position.y, transform.position.z);

            playerGameObject.GetComponent<FirstPersonController>().disabled = false;
        }
    }
    
}
