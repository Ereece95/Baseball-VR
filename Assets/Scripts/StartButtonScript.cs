using UnityEngine;
using System.Collections;

public class StartButtonScript : MonoBehaviour {

    public delegate void buttonHandler();
    public static event buttonHandler startButtonClicked;
    public static event buttonHandler exitButtonClicked;

    public void StartButtonClicked()    //must be public to see in the button's onClick() method
    {
        if(startButtonClicked != null )     //make sure someone is listening
            startButtonClicked();       //Fire the event
    }

}
