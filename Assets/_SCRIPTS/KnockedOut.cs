using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOut : MonoBehaviour
{
    private GameObject fadeTop;
    private GameObject fadeBottom;

    private GameObject player;
    private CamMouseLook CamMouseLook;
    private PlayerController playerController;

    bool knockedOutFinished = false;
    bool alreadyResetting = false;

    // Use this for initialization
    public void Start ()
    {
        Debug.Log("You've been knocked out...");
        knockedOutFinished = false;

        fadeTop = transform.Find("fadeTop").gameObject;
        fadeBottom = transform.Find("fadeBottom").gameObject;

        player = GameObject.Find("Character");

        //slightly rotates the player's z angle so that they can fall over
        player.transform.rotation = Quaternion.Euler(0, 0, -10.0f);

        //disbales the player controller script
        playerController = player.GetComponent<PlayerController>();
        playerController.enabled = false;

        //gets the FPPCamera and then disables the Cam Mouse Look script
        CamMouseLook = player.transform.Find("FPPCamera").GetComponent<CamMouseLook>();
        CamMouseLook.enabled = false;

        //unlocks the Z axis so that the player can fall over
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (!knockedOutFinished) {
            Transform fadeTopPos = fadeTop.transform;
            Transform fadeBottomPos = fadeBottom.transform;

            //Moves the top black overlay down to the centre
            if (fadeTop.GetComponent<RectTransform>().localPosition.y > -120)
                fadeTop.transform.position = Vector3.Lerp(fadeTopPos.position, new Vector3(fadeTopPos.position.x, fadeTopPos.position.y - 0.8f), 1500.0f * Time.deltaTime);

            //moves the bottom black overlay up to the centre
            if (fadeBottom.GetComponent<RectTransform>().localPosition.y < 120)
            {
                fadeBottom.transform.position = Vector3.Lerp(fadeBottomPos.position, new Vector3(fadeBottomPos.position.x, fadeBottomPos.position.y + 0.8f), 1500.0f * Time.deltaTime);
            } 
            else
            {
                knockedOutFinished = true;
            }
        }
        else
        {
            Debug.Log("Knocked out sequence finished.");
            resetPlayer();
        }
    }

    private GameObject findClosedPoint()
    {
        GameObject[] points;
        points = GameObject.FindGameObjectsWithTag("RespawnPoint");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        //gets the players current position
        Vector3 position = player.transform.position;

        //loop through each respawn point, looking for the closest one
        foreach (GameObject point in points)
        {
            Vector3 diff = point.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = point;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void resetPlayer()
    {
        if (!alreadyResetting) {
            alreadyResetting = true;

            //sets the player position to the nearest respawn point
            player.transform.position = findClosedPoint().transform.position;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            //re-enables the disabled scripts
            playerController.enabled = true;
            CamMouseLook.enabled = true;

            //Freezes all the rotation axis
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            //gets a reference to the health that's attached to the player and then reset the health
            Vitals vitals = player.GetComponent<Vitals>();
            vitals.setHealth(100);
        }

        Transform fadeTopPos = fadeTop.transform;
        Transform fadeBottomPos = fadeBottom.transform;

        //Moves the top black overlay down to the centre
        if (fadeTop.GetComponent<RectTransform>().localPosition.y < 1200)
            fadeTop.transform.position = Vector3.Lerp(fadeTopPos.position, new Vector3(fadeTopPos.position.x, fadeTopPos.position.y + 0.8f), 1500.0f * Time.deltaTime);

        //moves the bottom black overlay up to the centre
        if (fadeBottom.GetComponent<RectTransform>().localPosition.y > -1200)
        {
            fadeBottom.transform.position = Vector3.Lerp(fadeBottomPos.position, new Vector3(fadeBottomPos.position.x, fadeBottomPos.position.y - 0.8f), 1500.0f * Time.deltaTime);
        }
        else
        {
            //since the reset seqence has finished, destroy the script
            alreadyResetting = false;
            destroyScript();
        }
    }

    private void destroyScript()
    {
        //deletes this game object
        Destroy(gameObject);
    }
}
