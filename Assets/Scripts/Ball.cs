using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// The code for the ball following a path based on stats from a pitcher at a certain time in the throw animation and after it is hit it flies in a random direction with a random force(For now). It displays a flag when it hits the ground and a collider of the bat
public class Ball : MonoBehaviour
{
    public ControllerInput controllerInput;
    public GameObject ball;
    public GameObject hand;
    public GameObject player;
    public GameObject cameraRig;
   // public GameObject fireworks;
   // public GameObject homerunUIText;
    private TrailRenderer trail;
    public float seconds;
    int x = 0;
    public static bool flagVis;
    private DisplayFoulText dsplyFoul;   // for showing the player the ball is foul
    //private Transform[] path = new Transform[11];
   // private homeruntext homerunText;
    public Transform start;
    public Transform[] pathArray;
    private float speed;
    private float throwspeed;//, throws;
    public Text DistDisplay;
    bool hit;
    private Rigidbody RB;
    public Transform[] path;
    int num;
    float ringScale = 0;
    /// <summary>
    /// The type of pitch
    /// </summary>
    int Paths;
    public delegate void BallHit();
    public static event BallHit ballHit;
    public static event BallHit ballNotHit;
    private GameController gc;
    public Animation Throw;
    public Transform plate;
    /// <summary>
    /// The Stats for the pitchers
    /// </summary>
    public StatsScript stats;
    /// <summary>
    /// The Quadrent that the pitch will go too
    /// </summary>
    public int quadrent;
    List<GameObject> _flags = new List<GameObject>();
    public float timeStart;
    public float timeCount;
    public float timeEnd;
    public float y;
    bool countstrike = true;
    public bool side;
    public bool ballTravel = false;

    public const int EASYFASTBALL = 71;
    public const int EASYCURVEBALL = 57;
    public const int EASYSLIDER = 66;
    public const int EASYSINKER = 71;
    public const int EASYCHANGEUP = 66;
    public const int MEDIUMFASTBALL = 81;
    public const int MEDIUMCURVEBALL = 67;
    public const int MEDIUMSLIDER = 76;
    public const int MEDIUMSINKER = 81;
    public const int MEDIUMCHANGEUP = 76;
    public const int HARDFASTBALL = 91;
    public const int HARDCURVEBALL = 77;
    public const int HARDSLIDER = 86;
    public const int HARDSINKER = 91;
    public const int HARDCHANGEUP = 86;

    public delegate void hitEvent(int distance, bool isFoul, bool isHomerun, bool isCaught);    ///<Set up event
    public static event hitEvent distanceHit;

    void OnEnable()
    {
        UIEvents.easyButtonClicked += ChangespeedE;
        UIEvents.mediumButtonClicked += ChangespeedM;
        UIEvents.hardButtonClicked += ChangespeedH;

    }
    void OnDisable()
    {
        UIEvents.easyButtonClicked -= ChangespeedE;
        UIEvents.mediumButtonClicked -= ChangespeedM;
        UIEvents.hardButtonClicked -= ChangespeedH;
    }

    /// <summary>
    /// Get the pitch from the stats with Paths
    /// and set the flags for when the ball hits the ground to true
    /// </summary>
    void Awake()
    {

        cameraRig = GameObject.Find("[CameraRig]");
        side = true;

        if (side)
        {
            float x = cameraRig.transform.position.x;
            float y = cameraRig.transform.position.y;
            float z = cameraRig.transform.position.z;
            cameraRig.transform.position.Set(x - 5, y, z + 5);
        }
        else
        {
            float x = cameraRig.transform.position.x;
            float y = cameraRig.transform.position.y;
            float z = cameraRig.transform.position.z;
            cameraRig.transform.position.Set(x + 5, y, z - 5);
        }

        hit = false;
        trail = gameObject.GetComponent<TrailRenderer>();
        flagVis = true;
    }


    /// <summary>
    /// num gets the child count of the path for the pitch chosen
    ///while shifting it for the quadrant chosen
    /// </summary>
    void Start()
    {
        player = GameObject.Find("Firstbaseman");
        Paths = 0;
        quadrent = 0;

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
        plate = GameObject.Find("Home Plate").transform;
        dsplyFoul = GameObject.Find("FoulBallDisplay").GetComponent("DisplayFoulText") as DisplayFoulText;
       // homeruntext = GameObject.Find("HomerunDisplay").GetComponent("homeruntext") as homeruntext;
        // controllerInput.GetComponent("ControllerInput") as ControllerInput;

    }
    int i = 0;
    bool collideBat = false; //to tell update if ball collided with bat
    float num24 = 0f;
    bool contin = false;
    /// <summary>
    /// The ball follows the hand position until the animation is done
    /// The ball then iterates through the array that it was assigned too
    /// and when it collides with the bat the ball is sent in a random direction
    /// and with a random force with r
    /// </summary>
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

                if ((collideBat == true) && (gc.GetState() != States.WaitForInput) && (gc.GetState() != States.BallNotHit) && (gc.GetState() != States.BallHit))
                {
                    int r = (Random.Range(1500, 1600));
                    float hitForce = (1 * r);
                    hit = true;
                    RB.useGravity = true;


                    //Trying to access device velocity here from controllerInput script. Gets Null reference exception
                    Vector3 batSwing = controllerInput.GetVelocity();
                    Debug.Log("Batswing = " + batSwing);


                    //try this
                    //ball.transform.position = Vector3.Reflect(ball.transform.position, batSwing);

                    ////this
                    ////RB.AddForce(batSwing);

                    ////And this
                    //RB.AddForce(batSwing, ForceMode.Impulse);


                    ////Debug.Log("Bat velocity = " + batController.velocity.magnitude);
                    //Debug.Log(controlInput.GetVelocity());


                    //collideBat = false;
                    //if (ballHit != null) ballHit();



                    //This block will generate a random direction and angle for ball to travel
                    var rotationVector = transform.rotation.eulerAngles;
                    int rotationY = (Random.Range(10, 100));
                    int rotationX = (Random.Range(-10, -60));
                    rotationVector.y = rotationY;
                    rotationVector.x = rotationX;
                    transform.rotation = Quaternion.Euler(rotationVector);

                    RB.AddForce(transform.rotation * Vector3.forward * hitForce);
                    collideBat = false;
                    if (ballHit != null) ballHit();
                }
                if (hit)
                {
                    if (ballTravel)
                    {
                        float dist = 3.28084f * Vector3.Distance(start.position, ball.transform.position);
                        double dist2 = System.Convert.ToDouble(dist);
                        dist2 = System.Math.Round(dist2, 0);
                        DistDisplay.text = "Distance: " + (dist2.ToString()) + " ft";
                    }
                }
                if (!hit)
                {
                    ball.transform.position = Vector3.MoveTowards(ball.transform.position, path[i].position, step);
                }
            }

        }
        if (gc.GetState() == States.WaitForCollision)
        {
            timeEnd = Time.time;
        }
        if (gc.GetState() == States.BallHit || gc.GetState() == States.WaitForCollision || gc.GetState() == States.WaitForInput)
        {
            FindClosetPlayer((timeEnd - timeStart)).GetComponent<Renderer>().enabled = true;
        }
        else
        {
            FindClosetPlayer((timeEnd - timeStart));
        }
    }
    //Stop the ball when it hits catcher and registers a strike
    /// <summary>
    /// the ball stops when it hits the catcher and calls ballNotHit()
    /// </summary>
    /// <param name="collision">Test to see where it landed and whether it was a homerun or not</param>
    void OnTriggerEnter(Collider collision)
    {
        bool isHomerun = false;
        bool isFoul = false;
        bool isCaught = false;
        if (collision.tag == "Catcher")
        {
            if (ballNotHit != null)
            {
                ballNotHit();
                ball.GetComponent<MeshRenderer>().enabled = false;
                ball.GetComponent<SphereCollider>().enabled = false;


                countstrike = false;
            }
        }
        if ((collision.tag == "Homerun") && (gc.GetState() == States.WaitForCollision))
        {
            isHomerun = true;
            isFoul = false;
            RB.velocity = Vector3.zero;
            Debug.Log("Homerun");
            float distance = 3.28084f * (Vector3.Distance(plate.position, transform.position));
            if (distanceHit != null) distanceHit((int)distance, isFoul, isHomerun, isCaught);
           // homerunUIText.SetActive(true);
            //fireworks.SetActive(true);
        }
        if ((collision.tag == "Player") && gc.GetState() == States.WaitForCollision)
        {
            RB.velocity = Vector3.zero;
            isCaught = true;
            Debug.Log("Ball Caught");
            float distance = 3.28084f * (Vector3.Distance(plate.position, transform.position));
            if (distanceHit != null) distanceHit((int)distance, isFoul, isHomerun, isCaught);
        }
    }
    //Stop the ball from moving when it contacts the field
    /// <summary>
    /// the ball stops when it hits the ground
    /// Calculates the distance and places a flag so the user cna see where the ball touched the ground
    /// If isHomerun collision occurs start particle system
    /// </summary>
    /// <param name="Col">Used to know when ball hits the field and when to stop it</param>
    void OnCollisionEnter(Collision Col)
    {
        bool isFoul = false;
        bool isHomerun = false;
        bool isCaught = false;
        ballTravel = false;
        //if(Col.gameObject.name == "Foul Pole")
        //{
        //    isHomerun = true;
        //    isFoul = false;
            
        //}

        if ((Col.gameObject.name == "Field" || Col.gameObject.name == "Homerun") && gc.GetState() == States.WaitForCollision)
        {
            RB.velocity = Vector3.zero;
            if ((ball.transform.position.x >= 0) && (ball.transform.position.z >= 0))
            {
                isFoul = false;
                //Debug.Log("Fair ball" + ball.transform.position.x + " " + ball.transform.position.z);

            }
            else
            {
                isFoul = true;
                Debug.Log("Foul ball: X " + ball.transform.position.x + " " + ball.transform.position.z);
                dsplyFoul.displayFoulText();
            }

            // Add a flag where the ball hits
            GameObject flag = GameObject.CreatePrimitive(PrimitiveType.Cube);
            flag.GetComponent<BoxCollider>().enabled = false;
            if (isFoul)
            {
                flag.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                flag.GetComponent<Renderer>().material.color = Color.blue;
            }
            flag.transform.localScale = new Vector3(0.5f, 0.005f, 0.5f);
            flag.transform.position = ball.transform.position;
            _flags.Add(flag);

            ball.transform.position = flag.transform.position;

            float distance = 3.28084f * (Vector3.Distance(plate.position, transform.position));
            if (distanceHit != null) distanceHit((int)distance, isFoul, isHomerun, isCaught);
        }
        if (Col.gameObject.name == "baseball_bat_regular")
        {
            ballTravel = true;
            collideBat = true;
            timeStart = Time.time;
        }

    }

    void shift()
    {

        Debug.Log(quadrent);
        switch (quadrent)
        {
            case 1:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y + .3f, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y + .3f, path[j].transform.position.z + .2f);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;
            case 2:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y + .3f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y + .3f, path[j].transform.position.z);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;

            case 3:

                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y + .3f, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y + .3f, path[j].transform.position.z - .2f);
                    }
                }
                break;
            case 4:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y, path[j].transform.position.z + .2f);
                    }
                }
                break;
            case 5:

                break;
            case 6:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y, path[j].transform.position.z - .2f);
                    }
                }
                break;
            case 7:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y - .3f, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y - .3f, path[j].transform.position.z + .2f);
                    }
                }
                break;
            case 8:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y - .3f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y - .3f, path[j].transform.position.z);
                    }
                }
                break;
            case 9:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 1)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y - .3f, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y - .3f, path[j].transform.position.z - .2f);
                    }
                }
                break;

        }

    }

    /// <summary>
    /// Sets everything back to its initial value to rethrow the pitch
    /// </summary>
    public void rethrowpitch()
    {
       // homerunUIText.SetActive(false);
      //  fireworks.SetActive(false);
        countstrike = true;
        ball.GetComponent<MeshRenderer>().enabled = true;
        ball.GetComponent<SphereCollider>().enabled = true;
        ball.transform.position = hand.transform.position;

        x = 0;
        contin = false;
        trail.Clear();
        collideBat = false;
        RB.useGravity = false; //resets the ball physics for next pitch
        RB.velocity = Vector3.zero;
        trail.enabled = false;
        hit = false;
        i = 0;

        shiftback();
        //sets paths and quadrent equal to the new random values of the next pitch
        Paths = stats.getPitchType();
        quadrent = stats.setQuadrent(side);
        num = pathArray[Paths].childCount;

        path = new Transform[num];


        for (int j = 0; j < num; j++)
        {
            path[j] = pathArray[Paths].GetChild(j);

        }
        shift();
    }
    void shiftback()
    {
        switch (quadrent)
        {
            case 1:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y - .2f, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y, path[j].transform.position.z - .2f);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;
            case 2:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y - .3f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y - .3f, path[j].transform.position.z);
                    }


                }
                // path[num].transform.position = new Vector3(path[num].transform.position.x, path[num - 1].transform.position.y + .4f, path[num - 1].transform.position.z);
                break;

            case 3:

                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y - .3f, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y - .3f, path[j].transform.position.z + .2f);
                    }
                }
                break;
            case 4:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y, path[j].transform.position.z - .2f);
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
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y, path[j].transform.position.z + .2f);
                    }
                }
                break;
            case 7:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y + .3f, path[j].transform.position.z - .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x + .2f, path[j].transform.position.y + .3f, path[j].transform.position.z - .2f);
                    }
                }
                break;
            case 8:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y + .3f, path[j].transform.position.z);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x, path[j].transform.position.y + .3f, path[j].transform.position.z);
                    }
                }
                break;
            case 9:
                for (int j = 0; j < num; j++)
                {
                    if (num == 2 || j == num - 2)
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y + .3f, path[j].transform.position.z + .2f);
                    }
                    else
                    {
                        path[j].transform.position = new Vector3(path[j].transform.position.x - .2f, path[j].transform.position.y + .3f, path[j].transform.position.z + .2f);
                    }
                }
                break;

        }
    }
    void ChangespeedE()
    {
        //sets stats equal to Archer C's stats and sets the pitch and quadrent equal to a new random one
        stats = GameObject.Find("CSV2").GetComponent<StatsScript>() as StatsScript;

        Paths = stats.getPitchType();
        if (Paths == 1)
        {
            speed = EASYFASTBALL;
        }
        else if (Paths == 2)
        {
            speed = EASYCURVEBALL;
        }
        else if (Paths == 0)
        {
            speed = EASYCHANGEUP;
        }
        else if (Paths == 4)
        {
            speed = EASYSLIDER;
        }
        else if (Paths == 3)
        {
            speed = EASYSINKER;
        }

        quadrent = stats.setQuadrent(side);
    }
    /// <summary>
    /// Medium speed 10mph less than Arrieta stats
    /// </summary>
    void ChangespeedM()
    {
        //sets stats equal to Archer C's stats and sets the pitch and quadrent equal to a new random one
        stats = GameObject.Find("CSV2").GetComponent<StatsScript>() as StatsScript;

        Paths = stats.getPitchType();
        if (Paths == 1)
        {
            speed = MEDIUMFASTBALL;
        }
        else if (Paths == 2)
        {
            speed = MEDIUMCURVEBALL;
        }
        else if (Paths == 0)
        {
            speed = MEDIUMCHANGEUP;
        }
        else if (Paths == 4)
        {
            speed = MEDIUMSLIDER;
        }
        else if (Paths == 3)
        {
            speed = MEDIUMSINKER;
        }

        quadrent = stats.setQuadrent(side);
    }
    /// <summary>
    /// Pitcher Areitta J, used actual 2016 stats for speed
    /// </summary>
    void ChangespeedH()
    {
        //sets stats equal to Arietta J's stats and sets the pitch and quadrent equal to a new random one
        stats = GameObject.Find("CSV").GetComponent<StatsScript>() as StatsScript;

        Paths = stats.getPitchType();
        if (Paths == 1)
        {
            speed = HARDFASTBALL;
        }
        else if (Paths == 2)
        {
            speed = HARDCURVEBALL;
        }
        else if (Paths == 0)
        {
            speed = HARDCHANGEUP;
        }
        else if (Paths == 4)
        {
            speed = HARDSLIDER;
        }
        else if (Paths == 3)
        {
            speed = HARDSINKER;
        }

        quadrent = stats.setQuadrent(side);
    }

    public void hideBallFlags()
    {
        for (int i = 0; i < _flags.Count; ++i)
        {
            _flags[i].GetComponent<Renderer>().enabled = false;
        }
    }
    public void showBallFlags()
    {
        for (int i = 0; i < _flags.Count; ++i)
        {
            _flags[i].GetComponent<Renderer>().enabled = true;
        }
    }

    public string getPitchType()
    {
        string pitchType;
        switch (Paths)
        {
            case 0:
                pitchType = "Changeup";
                break;
            case 1:
                pitchType = "Fastball";
                break;
            case 2:
                pitchType = "Curveball";
                break;
            case 3:
                pitchType = "Sinker";
                break;
            case 4:
                pitchType = "Slider";
                break;
            default:
                pitchType = "";
                break;
        }
        return pitchType;
    }

    GameObject FindClosetPlayer(float timeCounter)
    {
        //float x = 10f;
        //float scale = (((9*)-1) + 5);
        Vector3 scale = transform.localScale;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = ball.transform.position;
        // x = x + 1f;
        y = (((4f * timeCounter) - 1f) + 5f);
        foreach (GameObject go in gos)
        {
           // scale.x = y;
           // scale.z = y;

          //  ringScale = ringScale + 0.005f;
           //  if(ringScale>20)
             {
                // ringScale = 20;
             }
            //  Debug.Log(l);
            go.transform.localScale = new Vector3(y, 0.01f, y);
            go.GetComponent<Renderer>().enabled = false;
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }

        }
        return closest;
    }
}