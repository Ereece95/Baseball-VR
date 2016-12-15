using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayPitch : MonoBehaviour
{

    public Canvas displayCanvas;
    public Button displayButton;
    public Text pitchText;
    public Ball ball;
    bool isHidden; // flag for when canvas is hidden
                   // Use this for initialization
    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("DisplayPitchCanvas").GetComponent<Canvas>();
        displayCanvas.enabled = false;
        isHidden = true;

        // creat button listener for on click
        displayButton = displayButton.GetComponent<Button>();

        // Get ball object to call get pitch type function
        ball = GameObject.Find("baseball_ball").GetComponent("Ball") as Ball;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHidden)
        {
            //TODO: add function to ball to obtain pitch type
            pitchText.text = "Pitch Type: ";
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

