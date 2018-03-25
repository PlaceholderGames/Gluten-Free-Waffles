using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilePhone : MonoBehaviour {

    public DayNightCycle dayNightCycle;
    public Transform player;
    public Transform playerCamera;
    public bool appClosed = false;

    private int selection = 0;
    private int oldSelection = 0;
    private bool errorMessage = false;
    private bool inApp = false;

    // Use this for initialization
    void Start () 
    {
        Vector3 phoneDimentions = this.GetComponent<Renderer>().bounds.size;

        dayNightCycle = GameObject.Find("DayAndNightSystem").GetComponent<DayNightCycle>();
        
        transform.parent.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("Player").transform.Find("UI Camera").GetComponent<Camera>();
        transform.parent.GetComponent<Canvas>().planeDistance = 1;

        //transform.Find("Minutes");
    }

    private void Update()
    {
        if (errorMessage == false)
            HomeScreenSelection();
        
        else
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                transform.Find("Notifications").Find("Update Message").gameObject.SetActive(false);
                errorMessage = false;
            }
        }

        if (inApp == true)
        {
            if (appClosed == true)
                openHome();
        }
           

            
    }

    void OnGUI()
    {
        transform.GetChild(1).GetComponent<TextMesh>().text = formatHours(dayNightCycle.getTime());
        transform.GetChild(2).GetComponent<TextMesh>().text = formatMins(dayNightCycle.getTime());
        transform.GetChild(3).GetComponent<TextMesh>().text = dayNightCycle.getDay().ToString();
    }

    void HomeScreenSelection ()
    {
        if (!inApp)
        {
            oldSelection = selection;

            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.UpArrow))
                selection = selection - 4;
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.LeftArrow))
                selection--;
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.RightArrow))
                selection++;
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.DownArrow))
                selection = selection + 4;

            if (selection < 0)
                selection = 0;
            if (selection > 21)
                selection = 21;

            transform.GetChild(4).GetChild(oldSelection).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(4).GetChild(oldSelection).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);

            transform.GetChild(4).GetChild(selection).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            transform.GetChild(4).GetChild(selection).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.grey);

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                Launch(selection);
        }
    }

    private void Launch(int selection)
    {
        switch(selection)
        {

            case 0:
                {
                    //Mail
                    AppUnavailable();
                    break;
                }
            case 1:
                {
                    //Calendar
                    AppUnavailable();
                    break;
                }
            case 2:
                {
                    //Photos
                    AppUnavailable();
                    break;
                }
            case 3:
                {
                    //Camera
                    AppUnavailable();
                    break;
                }
            case 4:
                {
                    //Maps
                    AppUnavailable();
                    break;
                }
            case 5:
                {
                    //Clock
                    AppUnavailable();
                    break;
                }
            case 6:
                {
                    //Weather
                    AppUnavailable();
                    break;
                }
            case 7:
                {
                    //News
                    AppUnavailable();
                    break;
                }
            case 8:
                {
                    //Contacts
                    AppUnavailable();
                    break;
                }
            case 9:
                {
                    //Inventory
                    transform.Find("Applications").Find("Inventory").gameObject.SetActive(true);
                    hideHome();
                    inApp = true;
                    break;
                }
            case 10:
                {
                    //Compass
                    AppUnavailable();
                    break;
                }
            case 11:
                {
                    //Journal
                    AppUnavailable();
                    break;
                }
            case 12:
                {
                    //Barker
                    AppUnavailable();
                    break;
                }
            case 13:
                {
                    //App Market
                    AppUnavailable();
                    break;
                }
            case 14:
                {
                    //Banking
                    AppUnavailable();
                    break;
                }
            case 15:
                {
                    //Find Friends
                    AppUnavailable();
                    break;
                }
            case 16:
                {
                    //Health
                    AppUnavailable();
                    break;
                }
            case 17:
                {
                    //Settings
                    AppUnavailable();
                    break;
                }
            case 18:
                {
                    //Phone
                    AppUnavailable();
                    break;
                }
            case 19:
                {
                    //Internet
                    AppUnavailable();
                    break;
                }
            case 20:
                {
                    //Messages
                    AppUnavailable();
                    break;
                }
            case 21:
                {
                    //Music
                    AppUnavailable();
                    break;
                }
        }
    }

    private void hideHome ()
    {
        //transform.GetChild(0)... change background here
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
    }

    private void openHome()
    {
        //transform.GetChild(0)... change background here
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);

        inApp = false;
        appClosed = false;
    }

    private void AppUnavailable()
    {
        transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
        errorMessage = true;
    }

    private string formatMins(int currentTime)
    {
        string newTime;
        int mins;
        currentTime %= 3600;
        mins = currentTime / 60;

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
        int hours;
        hours = currentTime / 3600;
        currentTime %= 3600;

        string hourUpdate;
        hourUpdate = hours.ToString();

        newTime = hourUpdate + ":";

        return newTime;
    }
}
