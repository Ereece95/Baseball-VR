using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    int x=0;

    void Awake()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        trail.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (x < 70)
        {
            ball.transform.position = hand.transform.position;
        }
        else
        {
            trail.enabled = true;
            ball.transform.Translate(0f * Time.deltaTime, -0.5f * Time.deltaTime, -2f * Time.deltaTime);

        }
        x++;

    }
}
