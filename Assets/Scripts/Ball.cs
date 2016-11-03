using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    int x = 0;
    public Transform target;
    public Transform[] path;
    public Transform[] pathArray;
    public float speed, throws;


    void Awake()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        trail.enabled = false;
    }
    int i = 0;
    // Update is called once per frame
    void Update()
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
            float step = speed * Time.deltaTime;
            trail.enabled = true;
            //when x reaches a spesific value it enables the tral and moves the ball


            if (ball.transform.position == path[i].position)
            {
                i++;
            }

            transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, step);
            if(Input.GetMouseButtonDown(0))
            {
                transform.position = Vector3.zero;
            }




            //ball.transform.Translate(0f, -0.03125f, -0.25f);

        }
        //increments x to determine when to relase the ball
        x++;

    }
}