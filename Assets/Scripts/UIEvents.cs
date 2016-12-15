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
    public static event buttonHandler pitchTypeButtonClicked;

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

    /// <summary>
    /// Send event when hide flags button clicked. Listened for in GameController
    /// </summary>
    public void FlagsButtonClicked()     //must be public to see in the button's onClick() method
    {
        if (flagsButtonClicked != null)  //make sure someone is listening
            flagsButtonClicked();        //Fire the event
    }

    public void PitchTypeButtonClicked()     //must be public to see in the button's onClick() method
    {
        if (pitchTypeButtonClicked != null)  //make sure someone is listening
            pitchTypeButtonClicked();        //Fire the event
    }
}
