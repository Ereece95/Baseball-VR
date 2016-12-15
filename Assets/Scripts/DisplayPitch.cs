using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TogglePanel : MonoBehaviour
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
        //displayButton.onClick.AddListener(toggleDisplay); // listens for button click

        // Get ball object to call get pitch type function
        ball = GameObject.Find("baseball_ball").GetComponent("Ball") as Ball;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHidden)
        {
            //TODO: add function call to ball to obtain pitch type
            pitchText.text = "Pitch Type: ";
            displayCanvas.enabled = true;
            isHidden = false;
        }
    }
    public void toggleDisplay()
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

