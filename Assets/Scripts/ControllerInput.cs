using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerInput : MonoBehaviour
{
    private AdjHeight height;
    private SteamVR_TrackedController controller;
    private GameController gc;
    public SteamVR_TrackedObject trackedObj;
    //public SteamVR_Controller.Device batController { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public SteamVR_Controller.Device batController;

    void Start()
    {
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        height = GameObject.Find("heightAdjCanvas").GetComponent("AdjHeight") as AdjHeight;
    }

    void OnEnable()
    {
        controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += HandleTriggerClicked;
        controller.PadClicked += HandlePadClicked;
        controller.Gripped += HandleGripClicked;
        trackedObj = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();
    }

    void OnDisable()
    {
        controller.TriggerClicked -= HandleTriggerClicked;
        controller.PadClicked -= HandlePadClicked;
        controller.Gripped -= HandleGripClicked;
    }

    void FixedUpdate()
    {
        batController = SteamVR_Controller.Input((int)trackedObj.index);
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
        Debug.Log("In pad clicked. e.padY = " + e.padY.ToString());
        Debug.Log("State = " + gc.GetState());
        if (e.padY < -0.5f && gc.GetState() == States.WaitForInput)
        {
            gc.HandlePadPressed();
        }
        if (e.padY > 0.5f && gc.GetState() == States.Init)
        {
            Debug.Log("Change feet/inches int");
            if (height.feetInches == true)
            {
                Debug.Log("Change feet up");

                height.changeHeightFeet(true);
            }
            else
            {
                Debug.Log("Change inches up");

                height.changeHeightInches(true);
            }
        }
        if (e.padY < -0.5f && gc.GetState() == States.Init)
        {
            Debug.Log("Change feet/inches int");
            if (height.feetInches == true)
            {
                Debug.Log("Change feet down");
                height.changeHeightFeet(false);
            }
            else
            {
                Debug.Log("Change inches down");
                height.changeHeightInches(false);
            }
        }
        if (e.padX < -0.5f && gc.GetState() == States.Init)
        {
            Debug.Log("Change to feet");
            height.switchHeightType(true);
        }
        if (e.padX > 0.5 && gc.GetState() == States.Init)
        {
            Debug.Log("Change to inches");
            height.switchHeightType(false);
        }
       

    }

    void HandleGripClicked(object sender, ClickedEventArgs e)
    {
        if (gc.GetState() == States.Orientation)
        {
            gc.HandleGripClicked();
        }
    }

    //public float getForce(float ballSpeed, SteamVR_Controller.Device batController)
    //{
    //    float force;
    //    float ballMass = 0.145f;
    //    float vel = batController.velocity.magnitude;
    //    force = ((ballMass * vel) - (ballMass * ballSpeed)) / 0.003f;

    //    return force;
    //}
    public Vector3 GetVelocity()
    {

        Debug.Log("Entered function");
        Vector3 vel = batController.velocity;
        //Vector3 vel = batController.angularVelocity;
        Debug.Log(batController.velocity.magnitude);
        return vel;
    }
}