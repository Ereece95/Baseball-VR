using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists
using System;
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
    ExitGame,
    ShowingGameStats,
    StatsGot
}


/// <summary>
/// Switch between states so it only runs certain items when in the correct state
/// States:Init,StartClick,MainScene,ThrowPitch,ThrowPitchDone,BallHit,BallNoHit,WaitForCollision,WaitForInput,Delay,ExitGame,StatsGot
/// </summary>
public class GameController : MonoBehaviour
{

    //Events we will produce
    public delegate void gameEventHandler(int scoreMod);    ///<Set up event
    public static event gameEventHandler pitchCompleted;   //Call pitchCompleted

    //Events we will listen for go in OnEnable()

    public static GameController gc = null; ///<Used for singleton design pattern

    private StateMachine<States> gcFSM;
    private Ball ball;
    private DisplayPitch dsplyPitch;
    private AudioClip hit;
    private AudioSource audioS;
    private UpdateStats stats;
    private GameObject audioObject;
    private GameObject Strike;
    private AudioSource audioStrike;
    private GameObject Cheer;
    private AudioSource audioCheer;
    private Animation pitch;
    private GameObject Pitcher;
    private GameObject startmenu;
    private GameObject startmenubg; //start menu background needs to be destroyed separately after start
    public GameObject endStats;
    private CanvasGroup endStatsCanvas;
    private CanvasGroup gameCanvas;
    private CanvasGroup hitstrikeCanvas;
    List<HitStats> hitStats = null;
    HitStats hs = null;
    Ball Send = null;
    private Text topStats1, topStats2, farthestHit, averageHit, battingAvgHit;
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

        ball = GameObject.Find("baseball_ball").GetComponent("Ball") as Ball;
        dsplyPitch = GameObject.Find("DisplayPitchButton").GetComponent("DisplayPitch") as DisplayPitch;

        audioObject = GameObject.Find("Audio Source");
        audioS = audioObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(audioObject);

        Strike = GameObject.Find("AudioStrike");
        audioStrike = Strike.GetComponent<AudioSource>();
        DontDestroyOnLoad(Strike);

        Cheer = GameObject.Find("AudioCheer");
        audioCheer = Cheer.GetComponent<AudioSource>();
        DontDestroyOnLoad(Cheer);

        startmenu = GameObject.Find("StartMenu");
        startmenubg = GameObject.Find("SF Scene Elements");

        //Initialize State Machine Engine		
        gcFSM = StateMachine<States>.Initialize(this, States.Init);

        hitStats = new List<HitStats>();

        topStats1 = GameObject.Find("Top1").GetComponent<Text>();
        topStats2 = GameObject.Find("Top2").GetComponent<Text>();
        farthestHit = GameObject.Find("Farthest").GetComponent<Text>();
        averageHit = GameObject.Find("Average").GetComponent<Text>();
        battingAvgHit = GameObject.Find("BattingAvg").GetComponent<Text>();


        endStats = GameObject.Find("EndStats");


        endStatsCanvas = endStats.GetComponent<CanvasGroup>();
        gameCanvas = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        hitstrikeCanvas = GameObject.Find("Stats").GetComponent<CanvasGroup>();

    }
    /// <summary>
    /// Events we will listen for
    /// </summary>
    void OnEnable()
    {

        UIEvents.easyButtonClicked += EventEasyButtonClicked;
        UIEvents.mediumButtonClicked += EventMediumButtonClicked;
        UIEvents.hardButtonClicked += EventHardButtonClicked;
        UIEvents.exitButtonClicked += EventExitButtonClicked;
        UIEvents.nextPitchClicked += EventNextPitchButton;
        UIEvents.flagsButtonClicked += EventFlagButton;
        UIEvents.pitchTypeButtonClicked += EventPitchTypeButton;
        UIEvents.endGameStatsClicked += EventDisplayExitStats;
        Ball.ballHit += EventBallHit;
        Ball.ballNotHit += EventBallNotHit;
        Ball.distanceHit += OnHitDistanceEvent;

    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        UIEvents.easyButtonClicked -= EventEasyButtonClicked;
        UIEvents.mediumButtonClicked -= EventMediumButtonClicked;
        UIEvents.hardButtonClicked -= EventHardButtonClicked;
        UIEvents.exitButtonClicked -= EventExitButtonClicked;
        UIEvents.nextPitchClicked -= EventNextPitchButton;
        UIEvents.flagsButtonClicked -= EventFlagButton;
        UIEvents.pitchTypeButtonClicked -= EventPitchTypeButton;
        UIEvents.endGameStatsClicked -= EventDisplayExitStats;
        Ball.distanceHit -= OnHitDistanceEvent;
        Ball.ballHit -= EventBallHit;
        Ball.ballNotHit -= EventBallNotHit;

    }
    /// <summary>
    /// Switch between states
    /// </summary>
    void Update()
    {

        var state = gcFSM.State;

        switch (state)
        {
            case States.Init:   //Wait until event happens
                gameCanvas.interactable = false;
                gameCanvas.alpha = 0;
                gameCanvas.blocksRaycasts = false;
                hitstrikeCanvas.interactable = false;
                hitstrikeCanvas.alpha = 0;
                hitstrikeCanvas.blocksRaycasts = false;
                break;

            case States.StartClick:
                gameCanvas.interactable = true;
                gameCanvas.alpha = 1;
                gameCanvas.blocksRaycasts = true;
                hitstrikeCanvas.interactable = true;
                hitstrikeCanvas.alpha = 1;
                hitstrikeCanvas.blocksRaycasts = true;
                gcFSM.ChangeState(States.ThrowPitch);
                break;


            case States.ThrowPitch:
                //play animation to throw pitch
                HandleThrowPitch();
                break;

            case States.ThrowPitchDone:
                //Wait for event to break out of this state (BallHit or BallNotHit)
                break;

            case States.BallHit:
                gcFSM.ChangeState(States.WaitForCollision);
                break;

            case States.BallNotHit:
                gcFSM.ChangeState(States.WaitForInput); //stays in this state until EventNextPitchButton
                                                        // or EventExitButtonClicked
                break;

            case States.WaitForCollision:
                break;

            case States.WaitForInput:
                break;

            case States.StatsGot:
                break;

            case States.ShowingGameStats:
                break;                                  //Stay here until end game stats button is clicked

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

    private void EventEasyButtonClicked()
    {

        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    private void EventMediumButtonClicked()
    {
        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    private void EventHardButtonClicked()
    {
        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    /// <summary>
    /// Exit Button was clicked
    /// </summary>
    private void EventExitButtonClicked()
    {
        //Debug.Log("In Exit Button CLicked Function");
        //try
        //{
        //DisplayExitStats();
        //}catch (Exception e)
        //{
        //    Debug.Log("Display Stats Failed: " + e.ToString());
        //}
        gcFSM.ChangeState(States.ExitGame);
    }

    /// <summary>
    /// Next Pitch Button Clicked
    /// TODO: Update to animation instead of reload scene
    /// </summary>
    private void EventNextPitchButton()
    {
        gcFSM.ChangeState(States.ThrowPitch);   //replay animation
    }

    /// <summary>
    /// State Machine Logic for ThrowPitch
    /// </summary>
    private void HandleThrowPitch()
    {
        Pitcher = GameObject.Find("WBP_pitch 1");
        if (Pitcher != null)
        {
            pitch = Pitcher.GetComponent<Animation>();
            pitch.Play("Take 001");
        }
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
        audioCheer.PlayOneShot(audioCheer.clip, 0.6F);
        gcFSM.ChangeState(States.BallHit);

    }
    private void EventBallNotHit()
    {
        audioStrike.PlayOneShot(audioStrike.clip, 0.7F);
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
    private void EventDisplayExitStats()
    {

        int count = 1;
        string stats1 = "";
        string stats2 = "";
        string farthest = "";
        string average = "Average Hit: ";
        int farthestInt = 0;
        // int numPitches = stats.GetNumPitches();
        int totalDistance = 0;
        int averageDistance = 0;
        float battingAverage = 0f;

        hitStats = hitStats.OrderByDescending(o => o.distance).ToList();

        foreach (HitStats hs in hitStats)
        {
            totalDistance = totalDistance + hs.distance;
            if (count <= 5)
            {
                if (count == 1)
                {
                    farthestInt = hs.distance;
                    farthest = "Farthest Hit: " + farthestInt + " Ft";
                }
                if (!hs.isFoul)
                {
                    stats1 = stats1 + count + ") " + hs.distance + " Ft\n";
                    count++;
                }
            }
            else if (count > 5 && count <= 10)
            {
                stats2 = stats2 + count + ") " + hs.distance + " Ft\n";
                count++;
            }
        }
        averageDistance = totalDistance / count;
        averageHit.text = average + averageDistance;
        topStats1.text = stats1;
        topStats2.text = stats2;
        farthestHit.text = farthest;

        //Fade in stats at end of game
        //for (float x = 0; x <= 1; x = +.1f)
        //{
        endStatsCanvas.alpha = 1;
        endStatsCanvas.interactable = true;
        endStatsCanvas.blocksRaycasts = true;
        //}

        gameCanvas.interactable = false;
        gameCanvas.alpha = 0;
        gameCanvas.blocksRaycasts = false;

        gcFSM.ChangeState(States.ShowingGameStats);

        //TODO: will need to hide other panels that are visible through this panel
        //Timer(280);
        //Timer myTimer = new Timer();
        //myTimer.Interval = 50000;
        //myTimer.Start();
    }
    private void EventFlagButton()
    {
        if (Ball.flagVis)
        {
            Ball.flagVis = false;
            ball.showBallFlags();
        }
        else if (!Ball.flagVis)
        {
            Ball.flagVis = true;
            ball.hideBallFlags();
        }
    }

    private void EventPitchTypeButton()
    {
        dsplyPitch.displayPitchType();
    }

}
