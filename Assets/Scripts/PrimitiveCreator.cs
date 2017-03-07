using UnityEngine;
using System.Collections;

public class PrimitiveCreator : MonoBehaviour
{
    private SteamVR_TrackedController _controller;
    private GameController gc;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        _controller = GetComponent<SteamVR_TrackedController>();
    }

    private void OnEnable()
    {
        _controller.TriggerClicked += HandleTriggerClicked;
        _controller.PadClicked += HandlePadClicked;
    }

    private void OnDisable()
    {
        _controller.TriggerClicked -= HandleTriggerClicked;
        _controller.PadClicked -= HandlePadClicked;
    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {

    }

    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {

    }

}