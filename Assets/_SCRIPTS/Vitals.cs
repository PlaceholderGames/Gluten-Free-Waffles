using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    [Range(1, 100)]
    public int health = 100;
    [Range(0, 100)]
    public int soberness = 100;

    private GameObject kockedOutObj;

    // Use this for initialization
    private void Start ()
    {
        kockedOutObj = GameObject.Find("_SCRIPTS/KockedOut");
    }
	
	// Update is called once per frame
	private void Update ()
    {
        debugRemoveHP();

        if (health == 0)
        {
            Debug.Log("Boi, you dead!");
        }
	}

    private void debugRemoveHP()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                print("HP lowered to 0.");
                health = 0;
            }
        }
    }
}
