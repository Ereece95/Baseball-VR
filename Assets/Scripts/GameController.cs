using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists
using MonsterLove.StateMachine;
using System;

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
    WaitForInput,
    Delay,
    ExitGame
}


/// <summary>
/// Switch between states so it only runs certain items when in the correct state
/// States:Init,StartClick,ThrowPitch,ThrowPitchDone,BallHit,BallNoHit,ExitGame
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
    private GameObject Strike;
    private AudioSource audioStrike;
    private GameObject startmenu;

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

        startmenu = GameObject.Find("StartMenu"); //to disable after start

        audioObject = GameObject.Find("Audio Source");
        audioS = audioObject.GetComponent<AudioSource>();
        DontDestroyOnLoad(audioObject);

        Strike = GameObject.Find("AudioStrike");
        audioStrike = Strike.GetComponent<AudioSource>();
        DontDestroyOnLoad(Strike);

        //Initialize State Machine Engine		
        gcFSM = StateMachine<States>.Initialize(this, States.Init);


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

    }
    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        UIEvents.startButtonClicked -= EventStartButtonClicked;
        UIEvents.nextPitchClicked -= EventNextPitchButton;
        UIEvents.nextPitchClicked -= EventNextPitchButton;
        Ball.ballHit -= EventBallHit;
        Ball.ballNotHit -= EventBallNotHit;

    }
    /// <summary>
    /// 
    /// </summary>
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
                //Wait for event to break out of this state (BallHit or BallNotHit)
                break;

            case States.BallHit:
                gcFSM.ChangeState(States.WaitForInput); //stays in this state until EventNextPitchButton
                                                        // or EventExitButtonClicked
                break;

            case States.BallNotHit:
                gcFSM.ChangeState(States.WaitForInput); //stays in this state until EventNextPitchButton
                                                        // or EventExitButtonClicked
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
        try
        {
            startmenu.SetActive(false);
            GameObject.Destroy(startmenu);
            gcFSM.ChangeState(States.StartClick);
        }
        catch (Exception e)
        {
            Debug.Log("destroy failed: " + e.ToString());
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
        audioStrike.PlayOneShot(audioStrike.clip, 0.7F);
        gcFSM.ChangeState(States.BallNotHit);
    }
    public States GetState()
    {
        return gcFSM.State;
    }
}
