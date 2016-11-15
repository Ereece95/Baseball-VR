using UnityEngine;
using System.Collections;


public class HitCam : MonoBehaviour {

    private Camera cam1;
    private Camera cam2;
    private Transform hitball;
    private GameController gc;

    // Use this for initialization
    /// <summary>
    /// Initiates the cameras
    /// </summary>
    void Start () {
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Hit View Camera").GetComponent<Camera>();
        hitball = GameObject.Find("baseball_ball").transform;
        cam1.enabled = true;
        cam2.enabled = false;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }
	
	// Update is called once per frame
    /// <summary>
    /// Switches cameras to follow ball until the travel is done
    /// </summary>
	void Update () {
        if (gc.GetState() == States.WaitForInput)
        {
            cam1.enabled = false;
            cam2.enabled = true;
            transform.LookAt(hitball);
        }
        if (gc.GetState() != States.WaitForInput)
        {
            cam1.enabled = true;
            cam2.enabled = false;
        }
	}
}
