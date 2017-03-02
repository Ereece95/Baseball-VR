using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Display what pitch is thrown
/// </summary>
public class DisplayPitch : MonoBehaviour
{

    public Canvas displayCanvas;
    public GameObject displayBackground;
    public Button displayButton;
    public Text pitchText;
    public Ball ball;
    public bool isHidden; // flag for when canvas is hidden
    string pitchType; //type of pitch obtained from ball
    
                  
    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("DisplayPitchCanvas").GetComponent<Canvas>();
        displayBackground = GameObject.Find("Pitch_Type").GetComponent<GameObject>();
        displayCanvas.enabled = false;
        isHidden = true;

        // creat button listener for on click
        displayButton = displayButton.GetComponent<Button>();

        // Get ball object to call get pitch type function
        ball = GameObject.Find("baseball_ball").GetComponent("Ball") as Ball;
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        //if pitch type is displayed, set pitch type at each new pitch and display
        if (!isHidden)
        {
            pitchType = ball.getPitchType();
            pitchText.text = "Pitch Type: " + pitchType;
        }
    }
    public void displayPitchType()
    {
        if (isHidden)
        {
            displayCanvas.enabled = true;
            isHidden = false;
        }
        else
        {
            displayCanvas.enabled = false;
            isHidden = true;
        }


    }
}

