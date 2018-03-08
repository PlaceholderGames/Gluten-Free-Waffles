using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour 
{
    public float dayLengthInMins, nightLengthInMins;
    public float startTimeInHours;

    private const int SECS_IN_DAY = 86400; //total seconds in 1 day
    private bool isDay = true; //day is 06:00 till 18:00
    private byte day = 1; //the number of days that have passed

    private float enteredSecs = 0; //the total time the player entered, converted to seconds
    private float currentSecs = 0; //the current time of day in the game, in seconds

    private enum TimeState { Sunrise = 0, Day = 0, Sunset = 0, Night = 0 }

    private Light sun;
    private GameObject[] streetLights;

    private GameObject starSystem;

    private GameObject moon;
    private Transform player;

    public int getTime()
    {
        return (int)currentSecs;
    }

    public byte getDay()
    {
        return day;
    }

    public int getTimeState() {
        return 0;
    }

    private void setTimeState(TimeState timeState)
    {
        /*
        switch(timeState)
        {
            case TimeState.Sunrise: break;
            case TimeState.Day: break;
            case TimeState.Sunset: break;
            case TimeState.Night: break;

            default:
                break;
        }
        */

    }

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

    void updateWorldLights()
    {
        if (!isDay) //if night
        {
            //sun.shadows = LightShadows.None;
            //sun.intensity = 0;

            foreach (GameObject streetLight in streetLights)
            {
                streetLight.SetActive(true);
            }
        }
        else //if day
        {
            //sun.shadows = LightShadows.Hard;
            //sun.intensity = 1;

            foreach (GameObject streetLight in streetLights)
            {
                streetLight.SetActive(false);
            }
        }
    }

    void updateSunRise() {
        //stores 05:00 and 07:00 as seconds
        const int START_RISE = 18000, END_RISE = 25200;

        if (currentSecs >= START_RISE && currentSecs <= END_RISE)
        {
            if (sun.intensity < 1)
            {
                sun.intensity += 0.4f * Time.deltaTime;
            }
        }
    }

    void updateSunSet()
    {
        //stores 17:00 and 19:00 as seconds
        const int START_SET = 61200, END_SET = 68400;

        if (currentSecs >= START_SET && currentSecs <= END_SET)
        {
            if (sun.intensity > 0)
            {
                sun.intensity -= 0.4f * Time.deltaTime;
            }
        }
    }

    void checkIfDay()
    {
        //stores 06:00 and 19:00 as seconds
        const int START_DAY = 18000, END_DAY = 68400;

        //Sets isDay to true or false depending on the time
        if (currentSecs >= START_DAY && currentSecs < END_DAY && !isDay)
        {
            setTimeState(TimeState.Day);
            isDay = true;
            updateWorldLights();
        }
        else if (currentSecs >= END_DAY && isDay)
        {
            isDay = false;
            updateWorldLights();
        }
    }

    // Use this for initialization
    void Start () {
        //self reference the sun component
        sun = GetComponent<Light>();

        //find the player object
        player = GameObject.Find("Character").transform;

        //finds the gameobject of the star system
        starSystem = GameObject.Find("Stars");

        //finds the moon object from the scene
        moon = GameObject.Find("Moon");

        //convert the minuites entered into seconds
        enteredSecs = dayLengthInMins * 60;

        //Sets the current time to the entered time and moves the sun to the correct starting position
        currentSecs = startTimeInHours * 3600;
        transform.RotateAround(Vector3.zero, Vector3.left, 15 * startTimeInHours);

        starSystem.transform.RotateAround(Vector3.zero, Vector3.left, 15 * startTimeInHours);

        //Gets all the street lights from the scene
        streetLights = GameObject.FindGameObjectsWithTag("StreetLight");
        updateWorldLights();
    }

    // Update is called once per frame
    void Update () 
    {
        //rotates the light around a pivot; in degrees per second
        if (currentSecs < SECS_IN_DAY) 
        {
            transform.RotateAround(Vector3.zero, Vector3.left, (360 / enteredSecs) * Time.deltaTime);

            starSystem.transform.Rotate(Vector3.left * (360 / enteredSecs) * Time.deltaTime);

            //Stores the current game time in seconds
            currentSecs += (SECS_IN_DAY / enteredSecs) * Time.deltaTime;
        } 
        else
        {
            //increments the day by 1 and set the time to 0
            day++;
            currentSecs = 0;
        }

        updateSunRise();
        updateSunSet();

        //checks if it's day or night
        checkIfDay();

        moon.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, player.position - transform.position, 10.0f, 0.0f));
        moon.transform.rotation = Quaternion.Euler(moon.transform.eulerAngles.x, moon.transform.eulerAngles.y + 180, moon.transform.eulerAngles.z);

        //updates the game clock
        //updateClock();
    }
}