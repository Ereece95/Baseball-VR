using UnityEngine;
using System.Collections;

public class ControllerInput : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private GameController gc;

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

    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        if (gc.GetState() == States.WaitForInput)
        {
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
}
