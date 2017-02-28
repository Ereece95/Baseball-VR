using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayFoulText : MonoBehaviour {

    public Canvas displayCanvas;
    public Text pitchText;
    private DisplayPitch dsplyPitch;
    private GameController gc;
    bool foul; // flag for when ball is foul
    bool disabledPitchType;

    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("FoulBallCanvas").GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        dsplyPitch = GameObject.Find("DisplayPitchButton").GetComponent("DisplayPitch") as DisplayPitch;
        displayCanvas.enabled = false;
        foul = true;
        disabledPitchType = false;
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        //if pitch type is displayed, set pitch type at each new pitch and display
        if (foul && (gc.GetState() == States.WaitForInput))
        {
            displayCanvas.enabled = true;
            if (dsplyPitch.isHidden == false)
            {
                disabledPitchType = true;
                dsplyPitch.displayPitchType();
            }
        }
        else
        {
            displayCanvas.enabled = false;
            foul = false;
            if (disabledPitchType == true)
            {
                disabledPitchType = false;
                dsplyPitch.displayPitchType();
            }
        }
    }
    public void displayFoulText()
    {
        foul = true;
    }
}
