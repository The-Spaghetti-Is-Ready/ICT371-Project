using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveJenga : MonoBehaviour
{
	Ray ray;
	RaycastHit hit;
	public LayerMask mask;

	public Rigidbody Jenga;
	Vector3 initialPosition;
	private float distance;
	public float Speed = 1;



	
	void Start(){
		
	}


	void Update(){
		//Vector3 mousePos = Input.mousePosition;
		//mousePos.z = 100f;
		//mousePos = cam.ScreenToWorldPoint(mousePos);
	
		

		if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			initialPosition = transform.position;
			Vector3 rayPoint = ray.GetPoint(0);

			distance = Vector3.Distance(transform.position, rayPoint);

		    
		}

		if (Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);

			if (Physics.Raycast(ray, out hit, 100, mask)){
				//Jenga.MovePosition(initialPosition + new Vector3(rayPoint.x, 0, rayPoint.z));
				Jenga.MovePosition(transform.position + new Vector3(rayPoint.x, 0, rayPoint.z) * Speed * Time.deltaTime);
			}
		}
	}

}
