using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    int x=0;
	// Use this for initialization
	void Start ()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (x < 70)
        {
            ball.transform.position = hand.transform.position;
        }
        else
        {
            ball.transform.Translate(0f * Time.deltaTime, -0.5f * Time.deltaTime, -2f * Time.deltaTime);

        }
        x++;

    }
}
