using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePhone : MonoBehaviour {

    public DayNightCycle dayNightCycle;
    public Transform player;
    public Transform playerCamera;
    private GUIStyle guiStyle = new GUIStyle();
    private string text;

    // Use this for initialization
    void Start () {
        
        transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
        dayNightCycle = GameObject.Find("Sun").GetComponent<DayNightCycle>();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.84f, Screen.height * 0.12f, 3));
        transform.position = worldPoint;

        transform.Rotate(Vector3.right, -90);
        transform.Rotate(Vector3.up, -110);
    }
	
	// Update is called once per frame
	void Update () {

	}
    void OnGUI()
    {
        Vector3 phoneDimentions = this.GetComponent<Renderer>().bounds.size;
        print(phoneDimentions);
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(position.x, Screen.height - position.y - 495, textSize.x + 100, textSize.y + 100), formatTime(dayNightCycle.getTime()));
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

        string hourUpdate, minuteUpdate;
        hourUpdate = hours.ToString();
        if (mins < 10)
            minuteUpdate = "0" + mins.ToString();
        else
            minuteUpdate = mins.ToString();
        newTime = hourUpdate + ":" + minuteUpdate;

        return newTime;
    }
}
