using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGUI : MonoBehaviour {
    public DayNightCycle dayNightCycle;
    public Camera camalam;
    private float deltaTime = 0.0f;
    private string txtTime = "TIME TEST";

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);


        //GUI.Label(new Rect(10, 30, 100, 20), "Day " + dayNightCycle.getDay().ToString());

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

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        
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
