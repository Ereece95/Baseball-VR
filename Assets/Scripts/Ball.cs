using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    private TrailRenderer trail;
    //variables for moving the ball along a path
    public float reach = 1.0f;
    public float speed = 5.0f;
    public int currentPoint=0;
    public Transform[] path;
    //

    int x = 0;
    

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
            trail.enabled = true;
           
            Vector3 dir =  path[currentPoint].position- transform.position;
        transform.position += dir * Time.deltaTime;
            if(dir.magnitude<=reach)
            {
                currentPoint++;
            }
           
               
               //ball.transform.Translate(0f, -0.03125f, -0.25f);
            
            

        }
        //increments x to determine when to relase the ball
        x++;
        
    }
    void onDrawBall()
    {
        for(int i=0;i<path.Length;i++)
        {
            if(path[i]!=null)
            {
                Gizmos.DrawSphere(path[i].position, reach);
            }
        }
    }

}
