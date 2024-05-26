using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

[RequireComponent(typeof(OnCollision))]

public class TeleportVariant : MonoBehaviour
{
    [SerializeField] 
    private Transform playerTransform;
    
    [SerializeField] 
    private Vector3 positionDelta;
    
    [SerializeField][Range(0.0f, 1.0f)] 
    private float teleportChance = 0.5f; // this will change depending on the sanity

    [SerializeField] 
    private List<Transform> teleportAnchors = new List<Transform>();

    [SerializeField] private float teleportCooldown;
    private bool isTeleporting = false;

    void Start()
    {
        
    }

    public void MovePlayer()
    {
        if (teleportChance < Random.Range(0.0f, 1.0f))
            return;

        if (!isTeleporting)
        {
            isTeleporting = true;
            // get random from list
            Transform teleportTransform = teleportAnchors[Random.Range(0, teleportAnchors.Count)];
       
            playerTransform.position = teleportTransform.position;
            // teleport cooldown?
            StartCoroutine(ResetTeleportFlag());
            Invoke("ResetTeleportFlag", teleportCooldown);
        }
    }
    
    IEnumerator ResetTeleportFlag()
    {
        yield return new WaitForSeconds(0.1f);
        isTeleporting = false;
    }
}