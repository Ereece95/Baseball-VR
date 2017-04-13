using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class homeruntext : MonoBehaviour
{

    public Canvas displayCanvas;
    //private DisplayPitch dsplyPitch;
    private GameController gc;
    bool homerun; // homerun has occured
    bool pS;
    //bool disabledPitchType;

    void Start()
    {
        // find canvas and disable on start
        displayCanvas = GameObject.Find("HomerunCanvas").GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        //dsplyPitch = GameObject.Find("DisplayPitchButton").GetComponent("DisplayPitch") as DisplayPitch;
        displayCanvas.enabled = false;
        homerun = true;
        ParticleSystem pS = GetComponent<ParticleSystem>();
        //disabledPitchType = false;
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        //if homerun it will display homerun
        if (homerun && (gc.GetState() == States.WaitForInput))
        {
            displayCanvas.enabled = true;
            //if (dsplyPitch.isHidden == false)
            //{
            //    disabledPitchType = true;
            //    dsplyPitch.displayPitchType();
            //}
        }
        // otherwise reenable pitch type
        else
        {
            displayCanvas.enabled = false;
            homerun = false;
            //if (disabledPitchType == true)
            //{
            //    disabledPitchType = false;
            //    dsplyPitch.displayPitchType();
            //}
        }
    }
    public void displayFoulText()
    {
        homerun = true;
    }
   
}
