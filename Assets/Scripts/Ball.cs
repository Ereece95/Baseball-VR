using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public GameObject ball;
    public GameObject hand;
    
    private TrailRenderer trail;
    //variables for moving the ball along a path
    
   
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
    int i = 0;
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
           
            if(ball.transform.position==path[i].position)
            {
                i++;
            }
            
            
      transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, 5 * Time.deltaTime);

        }
        //increments x to determine when to relase the ball
        x++;
        
    }
    

}
