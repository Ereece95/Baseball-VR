using UnityEngine;
using System.Collections;

public class NextPitchScript : MonoBehaviour
{

    public delegate void buttonHandler();
    public static event buttonHandler startButtonClicked;
    public static event buttonHandler nextPitchClicked;
    public static event buttonHandler exitButtonClicked;

    public void NextPitchClicked()    //must be public to see in the button's onClick() method
    {
        if (nextPitchClicked != null)     //make sure someone is listening
            nextPitchClicked();       //Fire the event
    }

}
