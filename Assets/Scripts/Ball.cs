using UnityEngine;
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
 
    /// <summary>
    /// The random number is set for whether curveball, changeup, and fastball
    /// and whether the ball is hit is set to false
    /// </summary>

    void Awake()
    {
        hit = false;
        trail = gameObject.GetComponent<TrailRenderer>();
        Paths = (Random.Range(0, 3));
    }

    // Use this for initialization
    /// <summary>
    /// num gets the child count of the random path of either changeup, curveball, or fastball chosen
    ///
    /// </summary>
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        trail.enabled = false;
        num = pathArray[Paths].childCount;

        path = new Transform[num];
    }
    int i = 0;
    // Update is called once per frame
    /// <summary>
    /// The ball follows the hand position until the animation is done
    /// The ball then iterates through the array that it was assigned too
    /// and when the spacebar is hit the ball is sent in a random direction
    /// and with a random force with r
    /// </summary>
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
    /// <summary>
    /// the ball stops when it hits the ground
    /// </summary>
    /// <param name="Col">Used to know when ball hits the field and when to stop it</param>
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.name == "Field")
        {
            RB.velocity = Vector3.zero;
        }
    }

}