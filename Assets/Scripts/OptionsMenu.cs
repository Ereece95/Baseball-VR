/* 
 * CS204L - Baseball Project - Spring 2017
 * Purpose:
 * This script gives functionality to the options menu.Upon start,
 * menu should disable itself.It should then display
 * a menu containing all buttons previously on game screen
 * when user presses down on the HTC Vive touchpad.
*/

using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Valve.VR;
using System;

public class OptionsMenu : MonoBehaviour
{

    public Canvas optionsCanvas;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller;

    bool isVisible;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        optionsCanvas = GameObject.Find("OptionsMenuCanvas").GetComponent<Canvas>();  // obtain the canvas to be displayed
        optionsCanvas.enabled = false; // disable options menu on start

        isVisible = false; // boolean flag set to false at start for toggle function
    }
    // Update is called once per frame
    void Update()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index); // gets controller location

        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (controller.GetAxis().y < 0.7f)
            {
                EnableMenu(); // calls function to toggle visibility of options menu if y axis < 0
            }
        }

    }
    public void EnableMenu() // toggle for menu display, user presses down on controller to open, down again to close.
    {
        if (isVisible)
        {
            optionsCanvas.enabled = false;
            isVisible = false;
        }
        else
        {
            optionsCanvas.enabled = true;
            isVisible = true;
        }
    }

    public static implicit operator GameObject(OptionsMenu v)
    {
        throw new NotImplementedException();
    }
}