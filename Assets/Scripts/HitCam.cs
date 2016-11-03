using UnityEngine;
using System.Collections;


public class HitCam : MonoBehaviour {

    private Camera cam;
    private Transform hitball = GameObject.Find("baseball_ball").transform;
    private GameController gcFSM;

    // Use this for initialization
    void Start () {
        cam = GameObject.Find("Hit View Camera").GetComponent<Camera>();
        //gcFSM = GameObject.Find("GameController").GetComponent<Script>();
    }
	
	// Update is called once per frame
	void Update () {
       // if (gcFSM.State == )
        transform.LookAt(hitball);
	}
}
