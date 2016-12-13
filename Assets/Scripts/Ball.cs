using UnityEngine;
using System.Collections;

/// <summary>
/// The code for the ball following a path at a certain time in the throw animation and after it is hit it flies in a random direction with a random force(For now). It also stops the ball when it hits the ground for now.
/// </summary>
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
    public delegate void BallHit();
    public static event BallHit ballHit;
    public static event BallHit ballNotHit;
    private GameController gc;
    public Animation Throw;
    /// <summary>
    /// The random number is set for whether curveball, changeup, and fastball
    /// and whether the ball is hit is set to false
    /// </summary>
    /// 
    void OnEnable()
    {
        UIEvents.nextPitchClicked += rethrowpitch;
    }
    void OnDisable()
    {
        UIEvents.nextPitchClicked -= rethrowpitch;
    }

    void Awake()
    {
        //pathArray[0] = transform.Find("Changeup path");
        //pathArray[1] = transform.Find("Fastball path");
        //pathArray[2] = transform.Find("Curveball path");
        hit = false;
        trail = gameObject.GetComponent<TrailRenderer>();
        Paths = (Random.Range(0, 5));
        

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
        for (int j = 0; j < num; j++)
        {
            path[j] = pathArray[Paths].GetChild(j);

        }
        shift();
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;

    }
    int i = 0;
    bool collideBat = false;
    // Update is called once per frame
    /// <summary>
    /// The ball follows the hand position until the animation is done
    /// The ball then iterates through the array that it was assigned too
    /// and when the spacebar is hit the ball is sent in a random direction
    /// and with a random force with r
    /// </summary>
    float num24 = 0f;
    bool contin = false;

    void Update()
    {
        if ((gc.GetState() != States.Init) && (gc.GetState() != States.StartClick))
        {
            //an if statment to have the ball released at a certain time

            //sets the position of the ball to the pitchers hand while the
            //throwing animation is running
            if (contin == false)
            {
                if (Throw["Take 001"].time < 1.40023f)
                {
                    //sets the position of the ball to the pitchers hand while the
                    //throwing animation is running
                    if (Throw["Take 001"].time != num24)
                    {
                        ball.transform.position = hand.transform.position;
                    }
                }
                else
                {
                    contin = true;
                }
            }
            else
            {
                float step = speed * Time.deltaTime;
                trail.enabled = true;
                //when x reaches a spesific value it enables the trail and moves the ball



                if (ball.transform.position.x == path[i].position.x)
                {
                    if (i != num - 1)
                    {
                        i++;
                    }
                }

            //if (Input.GetMouseButtonDown(0) && (gc.GetState() != States.WaitForInput) && (gc.GetState() != States.BallNotHit) && (gc.GetState() != States.BallHit))
            if (collideBat == true && (gc.GetState() != States.WaitForInput) && (gc.GetState() != States.BallNotHit) && (gc.GetState() != States.BallHit))
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

                if (ballHit != null) ballHit();
                collideBat = false;
            }

                if (!hit)
                {

                ball.transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, step);
                
            }

            }


        }
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
        if (Col.gameObject.name == "baseball_bat_regular")
        {
            collideBat = setTrue();
        }
    }
    //Stop the ball when it hits catcher and registers a strike
    /// <summary>
    /// the ball stops when it hits the catcher and calls ballNotHit()
    /// </summary>
    /// <param name="catcher"></param>
    void OnTriggerEnter(Collider catcher)
    {
        if (catcher.tag == "Catcher")
        {
            if (ballNotHit != null) ballNotHit();
        }
    }

    bool setTrue()
    {
        return true;
    }
    void shift()
    {
        int quadrent = (Random.Range(1, 10));

        switch (quadrent)
        {
            case 1:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .3f, path[j].transform.position.y + .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .3f, path[j].transform.position.y, path[j].transform.position.z);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;
            case 2:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y + .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y, path[j].transform.position.z);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;

            case 3:

                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y + .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;
            case 4:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .4f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .4f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;
            case 5:

                break;
            case 6:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;
            case 7:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .4f, path[j].transform.position.y - .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .4f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;
            case 8:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y - .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;
            case 9:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y - .2f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .3f, path[j].transform.position.y, path[j].transform.position.z);
                    }
                }
                break;

        }

    }
    void rethrowpitch()
    {
        ball.transform.position = hand.transform.position;
        x = 0;
        trail.Clear();
        RB.useGravity = false; //resets the ball physics for next pitch
        RB.velocity = Vector3.zero;
        trail.enabled = false;
        hit = false;
        i = 0;
    }
   
}