using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    int x=0;
    public Transform target;
    public Vector3 path;
    public float speed;
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
	void Update ()
    {
        //an if statment to have the ball released at a certain time
        if (x < 70)
        {
            //sets the position of the ball to the pitchers hand while the
            //throwing animation is running
            ball.transform.position = hand.transform.position;
        }
        else
        {
            //when x reaches a spesific value it enables the tral and moves the ball
            
            float step = speed * Time.deltaTime;
            trail.enabled = true;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //ball.transform.Translate(0f, -0.03125f, -0.25f);

        }
        //increments x to determine when to relase the ball
        x++;

    }
}
