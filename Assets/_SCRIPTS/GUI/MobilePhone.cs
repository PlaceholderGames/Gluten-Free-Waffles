using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilePhone : MonoBehaviour {

    public DayNightCycle dayNightCycle;
    public Transform player;
    public Transform playerCamera;
    private GUIStyle guiStyle = new GUIStyle();
    private string text;

    // Use this for initialization
    void Start () {

        Vector3 phoneDimentions = this.GetComponent<Renderer>().bounds.size;
        transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
        dayNightCycle = GameObject.Find("Sun").GetComponent<DayNightCycle>();

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.8f, Screen.height * 0.12f, 3));
        transform.position = worldPoint;

        //transform.Rotate(Vector3.right, -90);
        transform.Rotate(Vector3.up, -110);
    }

    void OnGUI()
    {
        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));

        //Draw time onto the phone
        Rect phoneTime = new Rect(position.x, position.y + 160, textSize.x + 100, textSize.y + 100);
        //GUI.Label phone = GUI.Label(phoneTime, formatTime(dayNightCycle.getTime()));
        
        transform.GetChild(2).GetComponent<Text>().text = formatHours(dayNightCycle.getTime());
        transform.GetChild(3).GetComponent<Text>().text = formatMins(dayNightCycle.getTime());
    }

    private string formatMins(int currentTime)
    {
        string newTime;
        int hours, mins, secs;
        hours = currentTime / 3600;
        currentTime %= 3600;
        mins = currentTime / 60;
        currentTime %= 60;
        secs = currentTime;

        string minuteUpdate;
       
        if (mins < 10)
            minuteUpdate = "0" + mins.ToString();
        else
            minuteUpdate = mins.ToString();
        newTime = minuteUpdate;

        return newTime;
    }

    private string formatHours(int currentTime)
    {
        string newTime;
        int hours, mins, secs;
        hours = currentTime / 3600;
        currentTime %= 3600;
        mins = currentTime / 60;
        currentTime %= 60;
        secs = currentTime;

        string hourUpdate;
        hourUpdate = hours.ToString();

        newTime = hourUpdate + ":";

        return newTime;
    }
}
