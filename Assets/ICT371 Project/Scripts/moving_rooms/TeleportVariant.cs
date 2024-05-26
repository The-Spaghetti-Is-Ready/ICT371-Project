using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

public class TeleportVariant : MonoBehaviour
{
    [Range(0.0f, 1.0f)] 
    public float teleportChance = 1.0f; // this will change depending on the sanity

    public string requiredTag = "";

    public List<Transform> teleportAnchors = new List<Transform>();

    public float _teleportCooldown;
    
    bool _isTeleporting = false;

    void MovePlayer(Transform playerTransform)
    {
        if (teleportChance < Random.Range(0.0f, 1.0f))
            return;

        if (!_isTeleporting)
        {
            _isTeleporting = true;
            // get random from list
            Transform teleportTransform = teleportAnchors[Random.Range(0, teleportAnchors.Count - 1)];
       
            playerTransform.position = teleportTransform.position;
            // teleport cooldown?
            StartCoroutine(ResetTeleportFlag());
            Invoke("ResetTeleportFlag", _teleportCooldown);
        }
    }
    
    IEnumerator ResetTeleportFlag()
    {
        yield return new WaitForSeconds(0.1f);
        _isTeleporting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (CanInvoke(other.gameObject))
            MovePlayer(other.transform);
    }

    bool CanInvoke(GameObject other)
    {
        return string.IsNullOrEmpty(requiredTag) || other.CompareTag(requiredTag);
    }
}