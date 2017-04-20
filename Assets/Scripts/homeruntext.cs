using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class homeruntext : MonoBehaviour
{

    public Canvas displayCanvas;
    private GameController gc;
    bool homerun; // homerun has occured
    //bool p5;

    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("HomerunCanvas").GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        displayCanvas.enabled = false;
        homerun = true;
        //ParticleSystem pS = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        //if homerun it will display homerun
        Debug.Log("homerunupdate");
        if (homerun && (gc.GetState() == States.WaitForInput))
        {
            displayCanvas.enabled = true;
            Debug.Log("inif");
            Debug.Log("state=" + gc.GetState());
            
        }
        // otherwise reenable pitch type
        else
        {
            Debug.Log("inelse");
            displayCanvas.enabled = false;
            homerun = false;
            Debug.Log("state=" + gc.GetState());
        }
    }
    public void HRText()
    {
        Debug.Log("enableHR");
        homerun = true;
    }
   
}
