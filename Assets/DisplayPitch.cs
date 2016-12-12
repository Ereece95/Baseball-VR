using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayPitch : MonoBehaviour {

    public Canvas displayCanvas;
    public Button displayButton;
    bool isHidden; // flag for when canvas is hidden
    // Use this for initialization
    void Start ()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("DisplayPitchCanvas").GetComponent<Canvas>();
        displayCanvas.enabled = false;
        isHidden = true;

        // creat button listener for on click
        Button dsplybtn = displayButton.GetComponent<Button>();
        dsplybtn.onClick.AddListener(toggleDisplay); // listens for button click
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void toggleDisplay()
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
