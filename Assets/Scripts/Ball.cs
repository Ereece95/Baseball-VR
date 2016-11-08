﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    int x = 0;
    //private Transform[] path = new Transform[11];
    public Transform[] pathArray;
    public float speed;//, throws;
    bool hit;
    private Rigidbody RB;
    public Transform[] path;
    int num;
    int Paths;

    void Awake()
    {
        hit = false;
        trail = gameObject.GetComponent<TrailRenderer>();
        Paths = (Random.Range(-1, 3));
    }

    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        trail.enabled = false;
        num = pathArray[Paths].childCount;

        path = new Transform[num];
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
            //when x reaches a spesific value it enables the trail and moves the ball

            for (int j = 0; j < num; j++)
            {
                path[j] = pathArray[Paths].GetChild(j);

            }

            if (ball.transform.position == path[i].position)
            {
                if (i != num - 1)
                {
                    i++;
                }
            }

            if (Input.GetKeyDown("space"))
            {
                int r = (Random.Range(600, 1800));
                float hitForce = (1 * r);
                hit = true;
                RB.useGravity = true;

                //This block will generate a random direction and angle for ball to travel
                var rotationVector = transform.rotation.eulerAngles;
                int rotationY = (Random.Range(0, 90));
                int rotationX = (Random.Range(-10, -60));
                rotationVector.y = rotationY;
                rotationVector.x = rotationX;
                transform.rotation = Quaternion.Euler(rotationVector);

                RB.AddForce(transform.rotation * Vector3.forward * hitForce);
            }

            if (!hit)
            {
                transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, step);
            }

        }
        //increments x to determine when to relase the ball
        x++;

    }
    //Stop the ball from moving when it contacts the field
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.name == "Field")
        {
            RB.velocity = Vector3.zero;
        }
    }

}