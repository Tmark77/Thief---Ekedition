using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float walkSpeed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float runSpeed = 8.0F;
    public float gravity = 20.0F;
	public float sneakSpeed;
	public float crouchSpeed;

	private float currentSpeed;

	private bool sneaking = false;
	private bool running = false;
	private bool crouching = false;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
			currentSpeed = walkSpeed;

			if (Input.GetButton ("Sneak"))
				sneaking = true;
			 else
				sneaking = false;
			if (Input.GetButtonDown ("Crouch")) 
				crouching = !crouching;
			if (Input.GetButton ("Run")) {
				running = true;
				sneaking = false;
			} else
				running = false;



			if (crouching) 
				currentSpeed += crouchSpeed;
			if (sneaking)
				currentSpeed += sneakSpeed;
			if (running)
				currentSpeed += runSpeed;



			moveDirection *= currentSpeed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
			
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
