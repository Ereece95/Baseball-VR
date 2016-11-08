using UnityEngine;
using System.Collections;


public class HitCam : MonoBehaviour {

    private Camera cam1;
    private Camera cam2;
    private Transform hitball;
    private GameController gcFSM;

    // Use this for initialization
    void Start () {
        cam1 = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam2 = GameObject.Find("Hit View Camera").GetComponent<Camera>();
        hitball = GameObject.Find("baseball_ball").transform;
        cam1.enabled = true;
        cam2.enabled = false;
        //gcFSM = GameObject.Find("GameController").GetComponent("GameController");
    }
	
	// Update is called once per frame
	void Update () {
        //if (gcFSM.State == BallHit)
        cam1.enabled = false;
        cam2.enabled = true;
        transform.LookAt(hitball);
	}
}
