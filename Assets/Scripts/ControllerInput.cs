using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private GameController gc;
    public SteamVR_TrackedObject trackedObj;
    public SteamVR_Controller.Device batController { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }

    void OnEnable()
    {
        controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += HandleTriggerClicked;
        controller.PadClicked += HandlePadClicked;
        //trackedObj = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedObject>();
     }

    void OnDisable()
    {
        controller.TriggerClicked -= HandleTriggerClicked;
        controller.PadClicked -= HandlePadClicked;
    }
  
    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (gc.GetState() == States.Orientation || gc.GetState() == States.WaitForInput)
        {
            gc.HandleTriggerClicked();
        }
    }

    void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        
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
