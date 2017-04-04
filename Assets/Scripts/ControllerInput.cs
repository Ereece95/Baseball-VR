using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private GameController gc;
    public SteamVR_TrackedObject trackedObj;
    //public SteamVR_Controller.Device batController { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public SteamVR_Controller.Device batController;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }

    void OnEnable()
    {
        controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += HandleTriggerClicked;
        controller.PadClicked += HandlePadClicked;
        controller.Gripped += HandleGripClicked;
    }

    void OnDisable()
    {
        controller.TriggerClicked -= HandleTriggerClicked;
        controller.PadClicked -= HandlePadClicked;
        controller.Gripped -= HandleGripClicked;
    }

    void FixedUpdate()
    {
       // batController = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        Debug.Log("In Trigger Clicked");
        if (gc.GetState() == States.WaitForInput)
        {
            Debug.Log("Sending to Game Controller to ThrowPitch");
            gc.HandleTriggerClicked();
        }
    }

    void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        if (e.padY < 0 && gc.GetState() != States.Init && gc.GetState() != States.StartClick)
        {
            gc.HandlePadClicked();
        }
        
    }

    void HandleGripClicked(object sender, ClickedEventArgs e)
    {
        if (gc.GetState() == States.Orientation)
        {
            gc.HandleGripClicked();
        }
    }

    public Vector3 GetVelocity()
    {

        Debug.Log("Entered function");
        Vector3 vel = batController.velocity;
        //Vector3 vel = batController.angularVelocity;
        Debug.Log(batController.velocity.magnitude);
        return vel;
    }
}
