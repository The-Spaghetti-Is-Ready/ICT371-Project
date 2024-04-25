using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;

public class RotatBlock : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public LayerMask mask;
    private Rigidbody blockbody;
    private float Rspeed = 40.0f;
    bool hitobject = false;

    void start()
    {
        blockbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, this.mask))
            {
                if (hit.transform.gameObject.name == this.transform.name)
                {
                    this.hitobject = true;
                }
            }

            if (this.hitobject)
            {
                

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(Vector3.forward * Rspeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.back * Rspeed * Time.deltaTime);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //blockbody.constraints = RigidbodyConstraints.None;
            //this.blockbody.useGravity = true;
            this.hitobject = false;
        }

    }
}
