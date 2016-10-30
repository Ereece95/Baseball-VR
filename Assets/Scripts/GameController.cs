using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists
using MonsterLove.StateMachine;

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
        Delay,
        ExitGame
    }

    private StateMachine<States> gcFSM;

    //Timer
    private float time = 0.0f;
    private float delay = 15.0f;    //15 seconds
    private int HandleNextPitch;

    void Awake()
    {
        //Implement a Singleton
        if (gc == null) gc = this;          //first instantiation
        else if (gc != this) Destroy(gameObject);  //kill it if another instance exists

        DontDestroyOnLoad(gameObject);  //persist across levels

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
        UIEvents.nextPitchClicked += EventNextPitchButton; //NextPitchButton is called every time the event fire
    }

    void OnDisable()
    {
        UIEvents.startButtonClicked -= EventStartButtonClicked;
        UIEvents.nextPitchClicked -= EventNextPitchButton;  //HandleStartButton is called every time the event fires
        UIEvents.nextPitchClicked -= EventNextPitchButton; //NextPitchButton is called every time the event fire
    }

    void Update()
    {
        var state = gcFSM.State;

        switch (state)
        {
            case States.Init:   //Wait until event happens
                break;

            case States.StartClick:
                SceneManager.LoadScene("TylerSceneDH");
                gcFSM.ChangeState(States.ThrowPitch);
                break;


            case States.ThrowPitch:
                //play animation to throw pitch
                HandleThrowPitch();
                break;

            case States.ThrowPitchDone:
                //Wait for event to break out of this state (Next Pitch Button or Exit Game)
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
        gcFSM.ChangeState(States.ExitGame);
    }
    private void EventNextPitchButton()
    {
        gcFSM.ChangeState(States.StartClick);   //TODO: Eventually want ThrowPitch here. This is a hack
    }

    /// <summary>
    /// State Machine Logic for ThrowPitch
    /// </summary>
    private void HandleThrowPitch()
    {
        //A timer to run the animation  //TODO: FIX this to be more efficient
        time += Time.deltaTime;

        Debug.Log("time= " + time.ToString());

        if (time < delay) return;

        time = 0f;
        gcFSM.ChangeState(States.ThrowPitchDone);

    }
}
