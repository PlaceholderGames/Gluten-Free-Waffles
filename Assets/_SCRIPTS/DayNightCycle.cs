using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour 
{
    public float dayLengthInMins;
    public float nightLengthInMins;
    public float startTimeInHours;

    private const int SECS_IN_DAY = 86400; //total seconds in 1 day
    private int day = 1; //the number of days that have passed
    private float enteredSecs = 0; //the total time the player entered, converted to seconds
    private float currentSecs = 0; //the current time of day in the game, in seconds

    void updateClock()
    {
        int convertTime = (int)currentSecs;
        int hours, mins, secs;

        //convert the game seconds into hours, mins and secs
        hours = convertTime / 3600;
        convertTime %= 3600;
        mins = convertTime / 60;
        convertTime %= 60;
        secs = convertTime;

        Debug.Log("Day:" + day + " H:" + hours + " M:" + mins + " S:" + secs);
    }

    //private Light[] lights;

    // Use this for initialization
    void Start () {
        /*lights = FindObjectsOfType(typeof(Light)) as Light[];
        foreach (Light light in lights) {
            light.intensity = 100;
            Debug.Log(light);
        }*/
        //Light light = GetComponent<Light>();
        //Debug.Log(light);

        //convert the minuites entered into seconds
        enteredSecs = dayLengthInMins * 60;

        //Sets the current time to the entered time and moves the sun to the correct starting position
        currentSecs = startTimeInHours * 3600;
        transform.RotateAround(Vector3.zero, Vector3.left, 15 * startTimeInHours);
    }

    // Update is called once per frame
    void Update () 
    {
        //rotates the light around a pivot; in degrees per second
        if (currentSecs < SECS_IN_DAY) 
        {
            transform.RotateAround(Vector3.zero, Vector3.left, (360 / enteredSecs) * Time.deltaTime);

            //Stores the current game time in seconds
            currentSecs += (SECS_IN_DAY / enteredSecs) * Time.deltaTime;

            //updates the game clock
            updateClock();
        } 
        else
        {
            //increments the day by 1 and set the time to 0
            day++;
            currentSecs = 0;
        }
    }
}