using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drunk : MonoBehaviour
{
    public float reduceDrunkSpeed = 30.0f;
    public float initialAlpha = 0.1f;

    private GameObject player;
    private Vitals vitals;
    private Camera FPPCamera;
    private Image drunkOverlay;
    private CanvasGroup drunkOverlayGroup;

    private float sensitivityX = 15.0f;
    private float sensitivityY = 15.0f;

    private float smooth = 2.0f;
    private float smoothXAxis;
    private float smoothYAxis;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private bool fovIncrease = true;
    private float fadeTime = 0.0f;

    // Use this for initialization
    private void Start()
    {
        //get some components & objects
        player = GameObject.Find("Character");
        vitals = player.GetComponent<Vitals>();
        FPPCamera = player.transform.Find("FPPCamera").GetComponent<Camera>();
        drunkOverlay = transform.Find("DrunkOverlay").GetComponent<Image>();
        drunkOverlayGroup = drunkOverlay.GetComponent<CanvasGroup>();

        //set the initial alpha value for the drunk overlay
        drunkOverlay.color = new Color(drunkOverlay.color.r, drunkOverlay.color.g, drunkOverlay.color.b, initialAlpha);

        reduceDrunkSpeed = 100.0f - vitals.getSoberness();
        print("speed = " + reduceDrunkSpeed);
    }

    private void Update()
    {
        //increases and lowers the fov over time
        if (fovIncrease) {
            FPPCamera.fieldOfView += 10.0f * Time.deltaTime;

            if (FPPCamera.fieldOfView >= 140.0f)
            {
                fovIncrease = false;
            }
        } 
        else
        {
            FPPCamera.fieldOfView -= 10.0f * Time.deltaTime;

            if (FPPCamera.fieldOfView <= 120.0f)
            {
                fovIncrease = true;
            }
        }

        vitals.setSoberness(vitals.getSoberness() + (1.0f * reduceDrunkSpeed * Time.deltaTime));
        //print("delta~ = " + (reduceDrunkSpeed * Time.deltaTime));

        //lerp the alpha of the drunk panel overlay
        float newAlpha = Mathf.Lerp(initialAlpha, 0.0f, fadeTime);
        drunkOverlay.color = new Color(drunkOverlay.color.r, drunkOverlay.color.g, drunkOverlay.color.b, newAlpha);

        //lerp the alpha of the black gradient around the camera
        drunkOverlayGroup.alpha = Mathf.Lerp(1.0f, 0.0f, fadeTime);

        //reduceDrunkSpeed = 100.0f - vitals.getSoberness();
        //print("speed = " + reduceDrunkSpeed);

        fadeTime += (1.0f / reduceDrunkSpeed) * Time.deltaTime;

        print("soberness = " + vitals.getSoberness());

        if (vitals.getSoberness() >= 100.0f) {
            //ensures the sober value is exactly 100.0f | 100%
            vitals.setSoberness(100.0f);
            print("You're sober again boi!");

            //destory this script, resetting the player back to normal
            destroyScript();
        }

    }

    // Update is called once per frame
    private void FixedUpdate ()
    {
        //lerp the x and y pos of the mouse as the player moves the mouse
        smoothXAxis = Mathf.Lerp(smoothXAxis, Input.GetAxis("Mouse X"), Time.deltaTime * smooth);
        smoothYAxis = Mathf.Lerp(smoothYAxis, Input.GetAxis("Mouse Y"), Time.deltaTime * smooth);

        rotationX += smoothXAxis * sensitivityX;
        rotationY += smoothYAxis * sensitivityY;

        FPPCamera.transform.localEulerAngles = new Vector3(-rotationY, rotationX, transform.localEulerAngles.z);
    }

    private void destroyScript()
    {
        //deletes this game object
        Destroy(gameObject);
    }
}
