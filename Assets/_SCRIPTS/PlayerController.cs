using System.Collections;
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

    private Rigidbody selfRigidBody;
    

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        selfRigidBody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        //selfRigidBody.AddForce(0, -10, 0, ForceMode.Force);
        
    }
	
	// Update is called once per frame
	void Update () {
        translation = Input.GetAxis("Vertical") * speed;
        strafe = Input.GetAxis("Horizontal") * speed;

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
        if (Input.GetKeyDown(KeyCode.Space) && onGround) //if space isn't being pressed, allows the player to jump
        {
            canJump = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            canSprint = true;
        }
        else
        {
            canSprint = false;
        }
        //transform.Translate(strafe, 0, translation);


        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }                
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }    
        }		
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
}
