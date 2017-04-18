using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltipScript : MonoBehaviour
{
    public string instructions = "Select feet/inches using the below arrows, adjust height value using the adjacent buttons";

    void OnGUI()
    {
        GUI.Button(new Rect(500, 50, 40, 25), new GUIContent("Help", instructions));
        GUI.Label(new Rect(570, 40, 100, 400), GUI.tooltip);
    }
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
