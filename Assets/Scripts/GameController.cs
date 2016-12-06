using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists
using System.Timers;
using System.Linq;
using UnityEngine.UI;
using MonsterLove.StateMachine;


/// <summary>
/// The states the game may be in
/// </summary>
public enum States
{
    Init = 0,
    StartClick,
    MainScene,
    ChooseOptions,
    ThrowPitch,
    ThrowPitchDone,
    BallHit,
    BallNotHit,
    WaitForCollision,
    WaitForInput,
    Delay,
    ExitGame
}


/// <summary>
/// Game Controller - Singleton pattern used to ensure only 1 game controller is instantiated
///     Sets up delegates for message passing
///     See: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
/// 
/// State Machine can be found at
///    https://github.com/thefuntastic/Unity3d-Finite-State-Machine
///    See specifically the built in Methods
/// </summary>
public class GameController : MonoBehaviour
{

    //Events we will produce
    public delegate void gameEventHandler(int scoreMod);    ///<Set up event
    public static event gameEventHandler pitchCompleted;   //Call pitchCompleted

    //Events we will listen for go in OnEnable()



    public static GameController gc = null; ///<Used for singleton design pattern




    private StateMachine<States> gcFSM;
    private AudioClip hit;
    private AudioSource audioS;
    private UpdateStats stats;
    private GameObject audioObject;
    public CanvasGroup endStatsCanvas;
    List<HitStats> hitStats = null;
    HitStats hs = null;
    private Text topStats1, topStats2;

    /// <summary>
    /// Implement Singleton
    /// Initialize StateMachine
    /// </summary>
    void Awake()
    {
        //Implement a Singleton
        if (gc == null) gc = this;          //first instantiation
        else if (gc != this) Destroy(gameObject);  //kill it if another instance exists

        DontDestroyOnLoad(gameObject);  //persist across levels

        audioObject = GameObject.Find("Audio Source");
        audioS = audioObject.GetComponent("AudioSource") as AudioSource;
        DontDestroyOnLoad(audioObject);

        //Initialize State Machine Engine		
        gcFSM = StateMachine<States>.Initialize(this, States.Init);

        hitStats = new List<HitStats>();
        topStats1 = GameObject.Find("Top1").GetComponent<Text>();
        topStats2 = GameObject.Find("Top2").GetComponent<Text>();
    }

    /// <summary>
    /// Events we will listen for
    /// </summary>
    void OnEnable()
    {

        UIEvents.startButtonClicked += EventStartButtonClicked;
        UIEvents.exitButtonClicked += EventExitButtonClicked;
        UIEvents.nextPitchClicked += EventNextPitchButton;
        Ball.ballHit += EventBallHit;
        Ball.ballNotHit += EventBallNotHit;
        Ball.distanceHit += OnHitDistanceEvent;
        if (audioS == null) Debug.Log("No AudioSource Found");
            
    }
     
    void OnDisable()
    {
        UIEvents.startButtonClicked -= EventStartButtonClicked;
        UIEvents.nextPitchClicked -= EventNextPitchButton;
        UIEvents.nextPitchClicked -= EventNextPitchButton;
        Ball.distanceHit -= OnHitDistanceEvent;
        Ball.ballHit -= EventBallHit;
        Ball.ballNotHit -= EventBallNotHit;

    }

    void Update()
    {
        var state = gcFSM.State;

        switch (state)
        {
            case States.Init:   //Wait until event happens
                break;

            case States.StartClick:
                SceneManager.LoadScene("MasterScene");
                gcFSM.ChangeState(States.ThrowPitch);
                break;


            case States.ThrowPitch:
                //play animation to throw pitch
                HandleThrowPitch();
                break;

            case States.ThrowPitchDone:
                //Wait for event to break out of this state (Next Pitch Button or Exit Game)
                break;

            case States.BallHit:
                gcFSM.ChangeState(States.WaitForCollision);
                break;

            case States.BallNotHit:
                gcFSM.ChangeState(States.WaitForInput); //stays in this state until EventNextPitchButton
                                                        // or EventExitButtonClicked
                break;
            case States.WaitForInput:
                break;

            case States.ExitGame:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
                break;

        }

    }

    /// <summary>
    /// Start Button was Clicked
    /// </summary>
    private void EventStartButtonClicked()
    {
        gcFSM.ChangeState(States.StartClick);
    }

    /// <summary>
    /// Exit Button was clicked
    /// </summary>
    private void EventExitButtonClicked()
    {
        DisplayExitStats();
        gcFSM.ChangeState(States.ExitGame);
    }

    /// <summary>
    /// Next Pitch Button Clicked
    /// TODO: Update to animation instead of reload scene
    /// </summary>
    private void EventNextPitchButton()
    {
        gcFSM.ChangeState(States.StartClick);   //TODO: Eventually want ThrowPitch here. This is a hack
    }

    /// <summary>
    /// State Machine Logic for ThrowPitch
    /// </summary>
    private void HandleThrowPitch()
    {
        gcFSM.ChangeState(States.ThrowPitchDone);
        Timer(15);  ///<Wait for animation to play

    }

    /// <summary>
    /// Co-routine implementing a simple Timer
    /// </summary>
    /// <param name="time">To get the ball to throw after a certain amount of time automatically</param>
    /// <returns></returns>
    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private void EventBallHit()
    {
        audioS.PlayOneShot(audioS.clip, 0.7F);
        gcFSM.ChangeState(States.BallHit);
        
    }
    private void EventBallNotHit()
    {
        gcFSM.ChangeState(States.BallNotHit);
    }
    public States GetState()
    {
        return gcFSM.State;
    }

    void OnHitDistanceEvent(int distance, bool isFoul, bool isHomerun)
    {
        gcFSM.ChangeState(States.WaitForInput);
        hs = new HitStats();
        hs.distance = distance;
        hs.isFoul = isFoul;
        hs.isHomerun = isHomerun;

        hitStats.Add(hs);
        Debug.Log("HIT ADDDED" + distance);          
                                  
    }
    void DisplayExitStats()
    {
        int count = 1;
        string stats1 = "";
        string stats2 = "";
        List<HitStats> sortedList = hitStats.OrderBy(o => o.distance).ToList();
      
            foreach (HitStats hs in sortedList)
            {
            if (count <= 5)
            {
                if (!hs.isFoul)
                {
                    stats1 = stats1 + count + ". " + hs.distance + "\n";
                    count++;
                }
            }
            if (count > 5 && count <= 10)
            {
                stats2 = stats2 + count + ". " + hs.distance + "\n";
                count++;
            }
            else
            {
                break;
            }
            }
        topStats1.text = stats1;
        topStats2.text = stats2;

        //Fade in stats at end of game
        for (float x = 0; x <= 1; x = +.1f)
        {
            endStatsCanvas.alpha = x;
        }
        //TODO: will need to hide other panels that are visible through this panel

        Timer myTimer = new Timer();
        myTimer.Interval = 5000;
        myTimer.Start();
    }
}
