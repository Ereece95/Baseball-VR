using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists
using System;
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
    Orientation,
    ThrowPitch,
    ThrowPitchDone,
    BallHit,
    BallNotHit,
    WaitForCollision,
    WaitForInput,
    Delay,
    ExitGame,
    ShowingGameStats,
    StatsGot,
    OptionsDisplay
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

    public static GameController gc = null;



    ///<Used for singleton design pattern
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
    private VideoCompar video;
    private VideoCompar videoCompare;
    private OptionsMenu optnsMenu;
    //private SteamVR_TrackedController _controller;
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

        //_controller = GameObject.Find("Controller (right)").GetComponent<SteamVR_TrackedController>();

        ball = GameObject.Find("baseball_ball").GetComponent("Ball") as Ball;
        stats = GameObject.Find("Stats").GetComponent("UpdateStats") as UpdateStats;
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

        optnsMenu = GameObject.Find("OptionsMenuCanvas").GetComponent("OptionsMenu") as OptionsMenu;

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

        video = GameObject.Find("Video").GetComponent("VideoCompar") as VideoCompar;
        videoCompare = GameObject.Find("VideoCompare").GetComponent("VideoCompar") as VideoCompar;
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
        UIEvents.flagsButtonClicked += EventFlagButton;
        UIEvents.pitchTypeButtonClicked += EventPitchTypeButton;
        UIEvents.endGameStatsClicked += EventDisplayExitStats;
        UIEvents.videoButtonClicked += EventDisplayVideo;
        UIEvents.videoCompareButtonClicked += EventDisplayVideoCompare;
        Ball.ballHit += EventBallHit;
        Ball.ballNotHit += EventBallNotHit;
        Ball.distanceHit += OnHitDistanceEvent;
        //_controller.TriggerClicked += HandleTriggerClicked;
        //_controller.PadClicked += HandlePadClicked;

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
        UIEvents.flagsButtonClicked -= EventFlagButton;
        UIEvents.pitchTypeButtonClicked -= EventPitchTypeButton;
        UIEvents.endGameStatsClicked -= EventDisplayExitStats;
        UIEvents.videoButtonClicked -= EventDisplayVideo;
        UIEvents.videoCompareButtonClicked -= EventDisplayVideoCompare;
        Ball.distanceHit -= OnHitDistanceEvent;
        Ball.ballHit -= EventBallHit;
        Ball.ballNotHit -= EventBallNotHit;
        //_controller.TriggerClicked -= HandleTriggerClicked;
        //_controller.PadClicked -= HandlePadClicked;

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
                HideCanvas(true);
                break;

            case States.StartClick:
                HideCanvas(false);
                gcFSM.ChangeState(States.Orientation);
                break;

            case States.Orientation:
                //Wait for event to break out of this state (trigger hit to reflect proper stance)
                Debug.Log("state = orientation state");
                break;

            case States.ThrowPitch:
                Debug.Log("state = throwpitch");
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
                gcFSM.ChangeState(States.WaitForInput); //stays in this state until triggerClicked
                                                        // or EventExitButtonClicked
                break;

            case States.WaitForCollision:
                break;

            case States.WaitForInput:
                Debug.Log("state = waitforinput state");
                break;

            case States.StatsGot:
                break;

            case States.ShowingGameStats:
                break;                                  //Stay here until end game stats button is clicked

            case States.OptionsDisplay:
                Debug.Log("state = waitforinput state");
                break;                                  // Wait for input

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
    /// A Start Button was Clicked
    /// </summary>
    private void EventEasyButtonClicked()
    {

        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    /// <summary>
    /// A Start Button was Clicked
    /// </summary>
    private void EventMediumButtonClicked()
    {
        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    /// <summary>
    /// A Start Button was Clicked
    /// </summary>
    private void EventHardButtonClicked()
    {
        DestroyImmediate(startmenu);
        DestroyImmediate(startmenubg);
        gcFSM.ChangeState(States.StartClick);


    }
    public void HandleTriggerClicked()
    {
        if (gc.GetState() == States.WaitForInput)
        {
            Debug.Log("Changing state to throw pitch");
            gcFSM.ChangeState(States.ThrowPitch);
        }
        else if (gc.GetState() == States.StartClick || gc.GetState() == States.Init)
        {
            gcFSM.ChangeState(States.Orientation);
        }
    }
    public void HandleGripClicked()
    {
        if (gc.GetState() == States.Orientation)
        {
            gcFSM.ChangeState(States.ThrowPitch);
        }
    }
    /// <summary>
    /// For when trackpad is clicked
    /// </summary>
    public void HandlePadClicked()
    {
        if ((gc.GetState() != States.StartClick) && (gc.GetState() != States.Init))
        {
            bool isVisible = optnsMenu.EnableMenu();

            if (!isVisible)
            {
                gcFSM.ChangeState(States.WaitForInput);
            }
            else
            {
                gcFSM.ChangeState(States.OptionsDisplay);
            }
        }
    }
    /// <summary>
    /// Exit Button was clicked
    /// </summary>
    private void EventExitButtonClicked()
    {
        gcFSM.ChangeState(States.ExitGame);
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
        ball.rethrowpitch();
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
        audioStrike.PlayOneShot(audioStrike.clip, 0.7F);
        gcFSM.ChangeState(States.BallNotHit);
    }
    public States GetState()
    {
        return gcFSM.State;
    }
    /// <summary>
    /// Display how far the ball has gone in feet button
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="isFoul"></param>
    /// <param name="isHomerun"></param>
    void OnHitDistanceEvent(int distance, bool isFoul, bool isHomerun, bool isCaught)
    {
        gcFSM.ChangeState(States.WaitForInput);
        hs = new HitStats();
        hs.distance = distance;
        hs.isFoul = isFoul;
        hs.isHomerun = isHomerun;
        hs.isCaught = isCaught;
        if (isHomerun == true)
        {
            audioCheer.PlayOneShot(audioCheer.clip, 0.6F);
        }
        hitStats.Add(hs);
        //Debug.Log("HIT ADDDED" + distance);

    }
    /// <summary>
    /// When leaving display stats for the player
    /// </summary>
    private void EventDisplayExitStats()
    {
        optnsMenu.EnableMenu();

        int count = 1;
        int numHits = 0;
        string stats1 = "";
        string stats2 = "";
        string farthest = "Farthest Hit: ";
        string average = "Average Hit: ";
        string batAvg = "Batting Average: ";
        int farthestInt = 0;
        float totalDistance = 0;
        float averageDistance = 0;
        float battingAverage = 0;

        hitStats = hitStats.OrderByDescending(o => o.distance).ToList();

        foreach (HitStats hs in hitStats)
        {
            if ((hs.isFoul == false) && (hs.isCaught == false) && (hs.distance > farthestInt))
            {
                farthestInt = hs.distance;
            }

            if (count <= 5)
            {

                if (!hs.isFoul)
                {
                    stats1 = stats1 + count + ") " + hs.distance + " ft";
                    if (hs.isCaught == true)
                    {
                        stats1 = stats1 + " -- Out";
                    }
                    stats1 = stats1 + "\n";
                    count++;
                    totalDistance = totalDistance + hs.distance;
                    numHits++;
                }
            }
            else if (count > 5 && count <= 10)
            {
                if (!hs.isFoul && !hs.isCaught)
                {
                    stats2 = stats2 + count + ") " + hs.distance + " ft\n";
                    if (hs.isCaught == true)
                    {
                        stats1 = stats1 + " -- Out";
                    }
                    stats1 = stats1 + "\n";
                    count++;
                    totalDistance = totalDistance + hs.distance;
                    numHits++;
                }
            }
        }
        if (stats.GetNumPitches() == 0)
        {
            battingAverage = 0;
        }
        else
        {
            battingAverage = (numHits / stats.GetNumPitches());
        }
        batAvg = batAvg + (Mathf.Round(battingAverage * 1000f) / 1000f);
        if ((count - 1) == 0)
        {
            averageDistance = 0;
        }
        else
        {
            averageDistance = totalDistance / (count - 1);
        }
        averageHit.text = average + (int)averageDistance + " Ft";
        topStats1.text = stats1;
        topStats2.text = stats2;
        farthest = farthest + farthestInt + " Ft";
        farthestHit.text = farthest;
        battingAvgHit.text = batAvg;


        endStatsCanvas.alpha = 1;
        endStatsCanvas.interactable = true;
        endStatsCanvas.blocksRaycasts = true;

        //disables game canvas with buttons and hits and strikes canvas
        HideCanvas(true);

        gcFSM.ChangeState(States.ShowingGameStats);


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

    private void EventDisplayVideo()
    {
        if (video.video1.enabled == false)
        {
            video.playVideo();
        }
        else if (video.video1.enabled == true)
        {
            video.video1.enabled = false;
        }
    }
    private void EventDisplayVideoCompare()
    {
        if (videoCompare.video1.enabled == false)
        {
            videoCompare.playVideo();
        }
        else if (videoCompare.video1.enabled == true)
        {
            videoCompare.video1.enabled = false;
        }
    }

    private void HideCanvas(bool yes)
    {
        if (yes)
        {
            gameCanvas.interactable = false;
            gameCanvas.alpha = 0;
            gameCanvas.blocksRaycasts = false;
            hitstrikeCanvas.interactable = false;
            hitstrikeCanvas.alpha = 0;
            hitstrikeCanvas.blocksRaycasts = false;
        }
        else
        {
            gameCanvas.interactable = true;
            gameCanvas.alpha = 1;
            gameCanvas.blocksRaycasts = true;
            hitstrikeCanvas.interactable = true;
            hitstrikeCanvas.alpha = 1;
            hitstrikeCanvas.blocksRaycasts = true;
        }
    }

}
