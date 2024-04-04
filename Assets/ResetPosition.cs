using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    CountingCollision countingcollision;

    Vector3 position;
    Quaternion rotation;

    public void PositionReset(){
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        rotation = gameObject.transform.rotation;
        countingcollision = GameObject.FindGameObjectWithTag("BlockCounter").GetComponent<CountingCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) || countingcollision.isCollidingLapping()){
            PositionReset();
        }
    }
}
