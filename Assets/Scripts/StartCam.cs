using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCam : MonoBehaviour {

    private Camera cam1;
    private Camera cam2;
    private Transform hitball;
    private GameController gc;

    // Use this for initialization
    /// <summary>
    /// Initiates the cameras
    /// </summary>
    void Start()
    {
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Start Camera").GetComponent<Camera>();
        cam1.enabled = false;
        cam2.enabled = true;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }

    // Update is called once per frame
    /// <summary>
    /// Switches cameras to follow ball until the travel is done
    /// </summary>
    void Update()
    {
        if (gc.GetState() == States.Init)
        {
            cam1.enabled = false;
            cam2.enabled = true;
        }
        if (gc.GetState() != States.Init)
        {
            cam1.enabled = true;
            cam2.enabled = false;
        }
    }
}

