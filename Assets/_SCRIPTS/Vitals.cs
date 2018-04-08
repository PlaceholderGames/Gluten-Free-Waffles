using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    [Range(1, 100)]
    public int health = 100;
    [Range(0, 100)]
    public int soberness = 100;
    [Range(0, 100)]
    public int energy = 100;

    private bool playerIsDead = false;

    public void setHealth(int newHealth)
    {
        health = newHealth;

        //if the new health is greater than 0 than set the player as alive
        if (health > 0)
            playerIsDead = false;
    }

    public int getHealth()
    {
        return health;
    }

    // Use this for initialization
    private void Start ()
    {
    }
	
	// Update is called once per frame
	public void Update ()
    {
        //debug command to instalty kill the player
        debugRemoveHP();

        if (health == 0 && !playerIsDead)
        {
            playerIsDead = true;

            GameObject knockedOutObj = Instantiate(Resources.Load("Prefabs/KnockedOut"), Vector3.zero, Quaternion.identity) as GameObject;
        }
    }

    private void debugRemoveHP()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.D) && !playerIsDead)
            {
                print("HP lowered to 0.");
                health = 0;
            }
        }
    }
}
