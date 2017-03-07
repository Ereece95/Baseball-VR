using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedController _controller;
    private GameController gc;

    void Awake()
    {
        _controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }

    void OnEnable()
    {
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.PadClicked += HandlePadClicked;
    }

    void OnDisable()
    {
        _controller.TriggerClicked -= HandleTriggerClicked;
        _controller.PadClicked -= HandlePadClicked;
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
}
