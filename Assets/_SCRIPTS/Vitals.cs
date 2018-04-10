using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    [Range(1, 100)]
    public int health = 100;
    [Range(0, 100)]
    public float soberness = 100;
    [Range(0, 100)]
    public int energy = 100;

    private bool playerIsDead = false;
    private bool playerIsDrunk = false;

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

    public void setSoberness(float newSoberness) {
        soberness = newSoberness;
    }

    public float getSoberness() {
        return soberness;
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

        //debug command to instalty make the player drunk
        debugMakeDrunk();

        if (health == 0 && !playerIsDead)
        {
            playerIsDead = true;
            GameObject knockedOutObj = Instantiate(Resources.Load("Prefabs/KnockedOut"), Vector3.zero, Quaternion.identity) as GameObject;
        }

        if (soberness <= 50 && !playerIsDrunk)
        {
            playerIsDrunk = true;
            GameObject drunkObj = Instantiate(Resources.Load("Prefabs/Drunk"), Vector3.zero, Quaternion.identity) as GameObject;
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

    private void debugMakeDrunk() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.E) && !playerIsDrunk) {
                print("Player is now drunk.");
                soberness = 0;
            }
        }
    }
}
