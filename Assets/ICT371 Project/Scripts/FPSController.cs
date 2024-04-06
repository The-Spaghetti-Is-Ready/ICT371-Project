//
//Created by Marco Garzon Lara on 14/03/24
//
// NOTE: just a basic FPS camera, some of your standard FPS params are missing (sprinting, jumping, etc.) since you play as a senior with alzheimer's in this game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //Will add a character controller component if it is missing from target object.
public class FPSController : MonoBehaviour
{
    /**
    *Controller movement params
    */
    public Camera playerCamera;
    public float walkSpeed = 6f;

    public float lookSpeed = 2f;
    public float lookXLimit =  45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterControl;
    float currentSpeedX = 0;
    float currentSpeedY = 0;
    float movementDirectionY = 0;

    // Start is called before the first frame update
    void Start()
    {
        characterControl = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement handler
        Vector3 forward =  transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        currentSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        currentSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
        movementDirectionY = moveDirection.y;
        moveDirection = (forward * currentSpeedX) + (right * currentSpeedY);

        //Mouse rotation handler
        characterControl.Move(moveDirection * Time.deltaTime);
        if(canMove) 
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
