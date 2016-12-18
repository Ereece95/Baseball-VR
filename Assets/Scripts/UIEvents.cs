using UnityEngine;
using System.Collections;

/// <summary>
/// Buttons for Easy, Medium, and Hard pitches as well as Exit, nextpitch, show pitch, and hide flags
/// </summary>
public class UIEvents : MonoBehaviour {

    public delegate void buttonHandler();
    public static event buttonHandler easyButtonClicked;
    public static event buttonHandler mediumButtonClicked;
    public static event buttonHandler hardButtonClicked;
    public static event buttonHandler nextPitchClicked;
    public static event buttonHandler exitButtonClicked;
    public static event buttonHandler flagsButtonClicked;
    public static event buttonHandler pitchTypeButtonClicked;
    public static event buttonHandler endGameStatsClicked;

    /// <summary>
    /// Send event when Easy button clicked. Listened for in GameController
    /// </summary>
    public void EasyButtonClicked()    //must be public to see in the button's onClick() method
    {
        if (easyButtonClicked != null)     //make sure someone is listening
            easyButtonClicked();       //Fire the event
    }
    /// <summary>
    /// Send event when Medium button clicked. Listened for in GameController
    /// </summary>
    public void MediumButtonClicked()    //must be public to see in the button's onClick() method
    {
        if (mediumButtonClicked != null)     //make sure someone is listening
            mediumButtonClicked();       //Fire the event
    }
    /// <summary>
    /// Send event when Hard button clicked. Listened for in GameController
    /// </summary>
    public void HardButtonClicked()    //must be public to see in the button's onClick() method
    {
        if (hardButtonClicked != null)     //make sure someone is listening
            hardButtonClicked();       //Fire the event
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
    /// <summary>
    /// Displays the type of pitch a fast, curve, changeup, slider, or sinker
    /// </summary>
    public void PitchTypeButtonClicked()     //must be public to see in the button's onClick() method
    {
        if (pitchTypeButtonClicked != null)  //make sure someone is listening
            pitchTypeButtonClicked();        //Fire the event
    }
    /// <summary>
    /// Displays stats like the farthest ball hit, average hits, average feet which the ball went
    /// </summary>
    public void EndGameStatsClicked()     //must be public to see in the button's onClick() method
    {
        if (endGameStatsClicked != null)  //make sure someone is listening
            endGameStatsClicked();        //Fire the event
    }
}
