using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayOrientation : MonoBehaviour {

    public Canvas displayCanvas;
    public Text pitchText;
    private DisplayPitch dsplyPitch;
    private GameController gc;
    bool disabledPitchType;

    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("OrientationCanvas").GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        dsplyPitch = GameObject.Find("DisplayPitchButton").GetComponent("DisplayPitch") as DisplayPitch;
        displayCanvas.enabled = false;
        disabledPitchType = false;
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        // If state is orientation, display orientation message & disable pitch type
        if (gc.GetState() == States.Orientation)
        {
            displayCanvas.enabled = true;
            if (dsplyPitch.isHidden == false)
            {
                disabledPitchType = true;
                dsplyPitch.displayPitchType();
            }
        }
        //else, reenable pitch type
        else
        {
            displayCanvas.enabled = false;
            if (disabledPitchType == true)
            {
                disabledPitchType = false;
                dsplyPitch.displayPitchType();
            }
            gc.enableOptions();
        }
    }
}
