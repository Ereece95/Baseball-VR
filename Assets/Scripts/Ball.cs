using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    int x=0;
    //private Transform[] path = new Transform[11];
    public Transform[] pathArray;
    public float speed;//, throws;

   
    void Awake()
    {
        trail = gameObject.GetComponent<TrailRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        trail.enabled = false;
    }
    int i = 0;
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
            float step = speed * Time.deltaTime;
            trail.enabled = true;
            //when x reaches a spesific value it enables the tral and moves the ball
            int Paths=(Random.Range(0, 2));

            int num = pathArray[Paths].childCount;

            Transform[] path = new Transform[num];

            for (int j = 0; j < num; j ++ )
            {
                path[j] = pathArray[Paths].GetChild(j);

            }
            

            if(ball.transform.position == path[i].position)
            {
                if (i < num)
                {
                    i++;
                }
                
            }
                
                    transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, step);


        }
        //increments x to determine when to relase the ball
        x++;

    }
   // void changePaths()
   // {
        
    //}
}
