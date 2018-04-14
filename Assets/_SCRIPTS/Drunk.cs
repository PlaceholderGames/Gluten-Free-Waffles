using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drunk : MonoBehaviour
{
    public float reduceDrunkSpeed = 20.0f; //In seconds, going from 0 - 100 soberness
    public float initialAlpha = 0.1f;

    private float actualSpeed;

    private Vitals vitals;
    private Camera FPPCamera;
    private Image drunkOverlay;
    private CanvasGroup drunkOverlayGroup;

    private float sensitivityX = 15.0f;
    private float sensitivityY = 15.0f;

    private float smooth = 1.0f;
    private float smoothXAxis;
    private float smoothYAxis;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private bool fovIncrease = true;
    private bool drunkEnding = false;
    private float fovModifier = 10.0f;
    private float fadeTime = 0.0f;
    private float drunkOverlayGroupAlpha = 1.0f;

    private float initialFOV;
    private float resetFOVTimer = 0.0f;

    // Use this for initialization
    private void Start()
    {
        //get some components & objects
        GameObject player = GameObject.Find("Character");
        vitals = player.GetComponent<Vitals>();
        FPPCamera = player.transform.Find("FPPCamera").GetComponent<Camera>();
        drunkOverlay = transform.Find("DrunkOverlay").GetComponent<Image>();
        drunkOverlayGroup = drunkOverlay.GetComponent<CanvasGroup>();

        vitals.setDrunkState(true);

        //get the initial fov
        initialFOV = FPPCamera.fieldOfView;

        UpdateDrunkness();
    }

    private void UpdateDrunkness() {
        //func is used to set the initial drunk values
        drunkEnding = false;
        fadeTime = 0.0f;
        smooth = 1.0f;
        initialAlpha = 0.1f;
        drunkOverlayGroupAlpha = 1.0f;
        fovModifier = 10.0f;

        //modifies the speed based off of the inital drunk value
        actualSpeed = reduceDrunkSpeed;
        if (vitals.getSoberness() > 0.0f) {
            float modifier = 100f / (100.0f - vitals.getSoberness());
            actualSpeed = reduceDrunkSpeed / modifier;
            initialAlpha = (initialAlpha / modifier) - 0.01f;
            drunkOverlayGroupAlpha = 1.0f / modifier;
        }

        //set the initial alpha value for the drunk overlay
        drunkOverlay.color = new Color(drunkOverlay.color.r, drunkOverlay.color.g, drunkOverlay.color.b, initialAlpha);
    }

    private void Update()
    {
        //increases and lowers the fov over time
        if (fovIncrease) {
            FPPCamera.fieldOfView += fovModifier * Time.deltaTime;

            if (FPPCamera.fieldOfView >= 140.0f)
            {
                fovIncrease = false;
            }
        } 
        else
        {
            FPPCamera.fieldOfView -= fovModifier * Time.deltaTime;

            if (FPPCamera.fieldOfView <= 120.0f)
            {
                fovIncrease = true;
            }
        }

        //each frame, increase the soberness of the player
        vitals.setSoberness((100.0f / actualSpeed) * Time.deltaTime);

        //slowly change the smooth value for the camera lerp each frame
        smooth = vitals.getSoberness() / 10.0f;

        //slowly lower the fov modifer each frame
        fovModifier -= ((10.0f / actualSpeed) * Time.deltaTime);

        //lerp the alpha of the drunk panel overlay
        float newAlpha = Mathf.Lerp(initialAlpha, 0.0f, fadeTime);
        drunkOverlay.color = new Color(drunkOverlay.color.r, drunkOverlay.color.g, drunkOverlay.color.b, newAlpha);
        //print("alpha = " + drunkOverlay.color.a);

        //lerp the alpha of the black gradient around the camera
        drunkOverlayGroup.alpha = Mathf.Lerp(drunkOverlayGroupAlpha, 0.0f, fadeTime);

        fadeTime += (1.0f / actualSpeed) * Time.deltaTime;

        if (vitals.getSoberness() >= 100.0f)
        {
            drunkEnding = true;

            //ensures the sober value is exactly 100.0f | 100%
            vitals.setSoberness(100.0f, true);

            //reset the player fov over time
            FPPCamera.fieldOfView = Mathf.Lerp(FPPCamera.fieldOfView, initialFOV, resetFOVTimer);
            resetFOVTimer += (1.0f / 100.0f) * Time.deltaTime;
        }

        //only destroy the script one the fov has finished resetting
        if (FPPCamera.fieldOfView - Mathf.Abs(initialFOV) < 1.0f && drunkEnding)
            destroyScript();
    }

    // Update is called once per frame
    private void FixedUpdate()
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
        vitals.setDrunkState(false);

        //deletes this game object
        Destroy(gameObject);

        print("You sober again boi!");
    }
}
