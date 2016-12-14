using UnityEngine;
using System.Collections;

/// <summary>
/// Buttons like start exit and next pitch for the UI
/// </summary>
public class UIEvents : MonoBehaviour {

    public delegate void buttonHandler();
    public static event buttonHandler startButtonClicked;
    public static event buttonHandler nextPitchClicked;
    public static event buttonHandler exitButtonClicked;
    public static event buttonHandler flagsButtonClicked;

    /// <summary>
    /// Send event when start button clicked. Listened for in GameController
    /// </summary>
    public void StartButtonClicked()    //must be public to see in the button's onClick() method
    {
        if (startButtonClicked != null)     //make sure someone is listening
            startButtonClicked();       //Fire the event
    }
    /// <summary>
    /// Send event when exit button clicked. Listened for in GameController
    /// </summary>
    public void ExitButtonClicked()    //must be public to see in the button's onClick() method
    {
        if (exitButtonClicked != null)     //make sure someone is listening
            exitButtonClicked();       //Fire the event
    }
    /// <summary>
    /// Send event when next pitch button clicked. Listened for in GameController
    /// </summary>
    public void NextPitchClicked()    //must be public to see in the button's onClick() method
    {
        if (nextPitchClicked != null)     //make sure someone is listening
            nextPitchClicked();           //Fire the event
    }

    public void FlagsButtonClicked()
    {
        Debug.Log("Clicked");

        if (Ball.flagVis)
        {
            Ball.flagVis = false;
           
        }
        else if (!Ball.flagVis)
        {
            Ball.flagVis = true;
            
        }
        if (flagsButtonClicked != null)
            flagsButtonClicked();
    }
}
