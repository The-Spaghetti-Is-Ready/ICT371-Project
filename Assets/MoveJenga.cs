using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;


public class MoveJenga : MonoBehaviour
{
	Ray ray;
	RaycastHit hit;
	public LayerMask mask;
	private Rigidbody blockbody;
	
	Vector3 initialPosition;

	public float Speed = 1;
	private float distance;
	bool hitobject = false;
	private float Rspeed = 20.0f;

    void Start(){
        blockbody = GetComponent<Rigidbody>();
    }


	void Update(){
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

			if (hitobject)
			{
                blockbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                this.blockbody.useGravity = false;
                hit.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            
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
			blockbody.constraints = RigidbodyConstraints.None;
			this.blockbody.useGravity = true;
            this.hitobject = false;
        }
	}
}
