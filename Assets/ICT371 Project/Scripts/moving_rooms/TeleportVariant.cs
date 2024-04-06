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
    private float teleportChance = 0.5f;

    void Start()
    {
        
    }

    public void MovePlayer()
    {
        if (teleportChance < Random.Range(0.0f, 1.0f))
            return;

        Vector3 pos = playerTransform.position;
        playerTransform.position = new Vector3(pos.x + positionDelta.x, pos.y + positionDelta.y, pos.z + positionDelta.z);
    }
}