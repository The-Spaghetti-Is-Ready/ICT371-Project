using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    
    [Header("Bedroom")]
    [SerializeField] private GameObject bedroomTrigger;
    [SerializeField] private List<GameObject> bedroomTeleportAnchors;

    [SerializeField] private float teleportCooldown;
    private int chanceOfBedroomChanging;
    private bool isTeleporting = false;
    
    
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == bedroomTrigger.name) && !isTeleporting)
        {
            isTeleporting = true;
            chanceOfBedroomChanging = Random.Range(0, 10);
            
            if (chanceOfBedroomChanging < 5) {
                Debug.Log("Teleporting to Bedroom Variant 1");
                teleportToRoom(bedroomTeleportAnchors[0]);
            }
            else {
                Debug.Log("Teleporting to Bedroom Variant 2");
                teleportToRoom(bedroomTeleportAnchors[1]);
            }
        }
        else
        {
            foreach (GameObject bedroomTeleportAnchor in bedroomTeleportAnchors)
            {
                if ((other.gameObject.name == bedroomTeleportAnchor.name) && !isTeleporting)
                {
                    isTeleporting = true;
                    teleportToRoom(bedroomTrigger);
                    break;
                }
            }
        }
    }

    private void teleportToRoom(GameObject room)
    {
        StartCoroutine(ResetTeleportFlag());
        Invoke("ResetTeleportFlag", teleportCooldown);
        
        playerGameObject.GetComponent<FirstPersonController>().disabled = true;
        transform.position = room.GetComponent<Transform>().position;
        playerGameObject.GetComponent<FirstPersonController>().disabled = false;
        
    }
    
    IEnumerator ResetTeleportFlag()
    {
        yield return new WaitForSeconds(0.1f);
        isTeleporting = false;
    }
}




