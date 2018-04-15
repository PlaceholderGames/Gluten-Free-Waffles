﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed; //speed variable
    public float normalSpeed; //normal speed
    public float sprintSpeed; //sprint speed
    private float translation; // forwards and backwards
    private float strafe; //left and right
    
    

    public int forceConst = 100; //Force which is applied to the rigidbody when jumping
    
    private bool canJump; //will be true when the player can jump
    private bool onGround = true; //if the player is on the ground this will be true
    private bool canSprint; //variable changed when the player does to sprint

    private bool jumpTimer = true;

    [SerializeField]
    private float jumpTime = 1f;

    private Rigidbody selfRigidBody;

    public void modifyNormalSpeed(float newSpeed)
    {
        //used to modify the default speed at run time
        normalSpeed = newSpeed;
    }

    public void modifySprintSpeed(float newSpeed)
    {
        //used to modify the default speed at run time
        sprintSpeed = newSpeed;
    }

    public float getNormalSpeed()
    {
        return normalSpeed;
    }

    public float getSprintSpeed()
    {
        return sprintSpeed;
    }
    
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        selfRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        if (canJump)
        {
            canJump = false;
            
            selfRigidBody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }
        if (canSprint)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        if (Input.GetAxis("Jump") != 0 && onGround && jumpTimer) //if space isn't being pressed, allows the player to jump
        {
            canJump = true;

            jumpTimer = false;
            Invoke("resetJumpTimer", jumpTime);
        }
        if (Input.GetAxis("Sprint") !=0 )
        {
            canSprint = true;
        }
        else
        {
            canSprint = false;
        }
        transform.Translate(strafe, 0, translation);		
	}
    void OnCollisionStay(Collision coll)
    {
        onGround = true;
    }

    void OnCollisionExit(Collision coll)
    {
        if (onGround)
        {
            onGround = false;
        }
    }

    void resetJumpTimer()
    {
        jumpTimer = true;
    }
}
