using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        ball.transform.position = hand.transform.position;

    }
}
