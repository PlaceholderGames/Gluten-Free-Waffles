using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGUI : MonoBehaviour {
    public DayNightCycle dayNightCycle;
    public Camera camalam;

    private string txtTime = "TIME TEST";

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), formatTime(dayNightCycle.getTime()));
        GUI.Label(new Rect(10, 30, 100, 20), "Day " + dayNightCycle.getDay().ToString());

        Ray checkForItem = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit found;

        if (Physics.Raycast(checkForItem, out found, camalam.GetComponent<PickupDrop>().itemRange) && found.transform.tag == "item")
            GUI.Label(new Rect(10, 50, 300, 20), "Press E to Interact");
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }

    private string formatTime(int currentTime)
    {
        string newTime;
        int hours, mins, secs;
        hours = currentTime / 3600;
        currentTime %= 3600;
        mins = currentTime / 60;
        currentTime %= 60;
        secs = currentTime;

        newTime = hours.ToString() + "h " + mins.ToString() + "m " + secs.ToString() + "s";

        return newTime;
    }
}
