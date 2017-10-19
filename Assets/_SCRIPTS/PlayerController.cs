using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 10.0F;

    public int forceConst = 50; //Force which is applied to the rigidbody when jumping
    
    private bool canJump;
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
    }
	
	// Update is called once per frame
	void Update () {
        float jumpSpeed = 100;
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyUp(KeyCode.Space)) //if space isn't being pressed, allows the player to jump
        {
            canJump = true;
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
}
