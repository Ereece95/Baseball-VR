using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
	// Use this for initialization
	void Start () {
        var ballTransform = ball.transform;
        ballTransform.parent = transform.Find("R Hand");
        ballTransform.localPosition = Vector3.zero;
        ballTransform.localRotation = Quaternion.identity;
        ballTransform.localScale = Vector3.one;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
