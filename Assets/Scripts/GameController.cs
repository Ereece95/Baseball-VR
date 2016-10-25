using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;   //Lists

/// <summary>
/// Game Controller - Singleton pattern used to ensure only 1 game controller is instantiated
///     Sets up delegates for message passing
///     See: https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
/// </summary>
public class GameController : MonoBehaviour
{

    //Events we will product
    public delegate void gameEventHandler(int scoreMod);    ///<Set up event
    public static event gameEventHandler pitchCompleted;   //Call pitchCompleted

    //Events we will listen for go in OnEnable()



    public static GameController gc = null; ///<Used for singleton design pattern


    /// <summary>
    /// The states the game may be in
    /// </summary>
    public enum gameEvents
    {
        inactive = 0,
        pitchBeforeThrow,
        pitchBeingThrown,
        pitchThrown,
        pitchCaught,
        pitchHit,
        loadScene
    }

    public gameEvents currentEvent = gameEvents.inactive;   //Start inactive

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


    }

    /// <summary>
    /// Events we will listen for
    /// </summary>
    void OnEnable()
    {
        NextPitchScript.nextPitchClicked += HandleNextPitchButton; //NextPitchButton is called every time the event fire
    }

    void OnDisable()
    {
        NextPitchScript.nextPitchClicked -= HandleNextPitchButton;  //HandleStartButton is called every time the event fires
    }

    void Start()
    {
    }

    void Update()
    {

        switch (currentEvent)
        {
            case gameEvents.inactive:
                currentEvent = gameEvents.loadScene;
                break;

            case gameEvents.loadScene:
                SceneManager.LoadScene("STARTMENU");
                currentEvent = gameEvents.pitchThrown;
                time = 0;
                break;

            case gameEvents.pitchThrown:
                //play animation to throw pitch
                HandlePitchThrown();
                break;

            case gameEvents.pitchHit:
                //play hit animation
                break;

        }

    }

    private void HandleNextPitchButton()
    {
        SceneManager.LoadScene("TylerScene");
        currentEvent = gameEvents.pitchThrown;
    }

    private void HandlePitchThrown()
    {
        //A timer to run the animation  //TODO: FIX this to be more efficient
        time += Time.deltaTime;

        Debug.Log("time= " + time.ToString());

        if (time < delay) return;

        time = 0f;
        SceneManager.LoadScene("TylerScene");



    }
}
