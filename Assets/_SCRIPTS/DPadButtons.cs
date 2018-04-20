using UnityEngine;
using System.Collections;

/// <summary> This class maps the DPad axis to buttons. </summary>
public class DPadButtons : MonoBehaviour
{
    public static bool up;
    public static bool down;
    public static bool left;
    public static bool right;

    private float lastX, lastY;

    void Start()
    {
        up = down = left = right = false;
        lastX = lastY = 0;
    }

    void Update()
    {
        float lastDpadX = lastX;
        float lastDpadY = lastY;

        if (Helpers.IsAxisActive(AxisName.DPad_Horizontal))
        {
            float DPadX = Input.GetAxis(AxisName.DPad_Horizontal);

            if (DPadX == 1 && lastDpadX != 1) { right = true; } else { right = false; }
            if (DPadX == -1 && lastDpadX != -1) { left = true; } else { left = false; }

            lastX = DPadX;
        }
        else { lastX = 0; }

        if (Helpers.IsAxisActive(AxisName.DPad_Vertical))
        {
            float DPadY = Input.GetAxis(AxisName.DPad_Vertical);
            if (DPadY == 1 && lastDpadY != 1) { up = true; } else { up = false; }
            if (DPadY == -1 && lastDpadY != -1) { down = true; } else { down = false; }

            lastY = DPadY;
        }
        else { lastY = 0; }
    }
}