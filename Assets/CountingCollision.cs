using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingCollision : MonoBehaviour
{
    int overlapping;
    bool isover = false;

    public bool isCollidingLapping() {
        if (overlapping > 3){
            return isover = true;
        }
        else{
            return isover = false;
        }
        
    }

    void OnTriggerEnter(Collider other) {
        overlapping++;
    }

    void OnTriggerExit(Collider other) {
        overlapping--;
    }
}
