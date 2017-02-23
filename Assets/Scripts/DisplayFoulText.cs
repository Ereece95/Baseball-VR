using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayFoulText : MonoBehaviour {

    public Canvas displayCanvas;
    public Text pitchText;
    private GameController gc;
    bool foul; // flag for when ball is foul

    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("FoulBallCanvas").GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        displayCanvas.enabled = false;
        foul = true;
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
        }
        else
        {
            displayCanvas.enabled = false;
            foul = false;
        }
    }
    public void displayFoulText()
    {
        foul = true;
    }
}
