using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10F;
    public float sprintSpeed = 20F;
    private bool canSprint;
    

    public int forceConst = 100; //Force which is applied to the rigidbody when jumping
    
    private bool canJump;
    private bool onGround = true;
    private Rigidbody selfRigidBody;
    

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        selfRigidBody = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
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
            speed = 10F;
        }
    }
	
	// Update is called once per frame
	void Update () {

         float jumpSpeed = 300;


        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

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

        if (Input.GetKeyDown("escape"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }                
            else if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
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
