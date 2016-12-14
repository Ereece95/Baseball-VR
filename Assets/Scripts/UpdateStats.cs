//Tyler Owens
//Will temporarily increment the in-game stat count.
//Will need to be integrated further when an actually hit is determined
//or if the pitch is a ball or strike when it passes the plate.
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Increment the balls hit and the misses
/// </summary>
public class UpdateStats : MonoBehaviour {

  
    int hits = 0, strikes = 0;
    [SerializeField]
    private Text hitsText, strikesText;
   // private GameController gc;

    void OnEnable()
    {
        Ball.ballHit += EventBallHit;
        Ball.ballNotHit += EventBallNotHit;
       // gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }

    void OnDisable()
    {
        Ball.ballHit -= EventBallHit;
        Ball.ballNotHit -= EventBallNotHit;
    }

    void Awake()
    {
       DontDestroyOnLoad(gameObject);  //Get score to persist
       
    }

    void Start()
    {

        //TODO: Move this to start but requires object be visible on startmenu
        //if (gc.GetState() == States.StartClick)
        //{
        //    hitsText = GameObject.Find("HitsScore").GetComponent<Text>();
        //    strikesText = GameObject.Find("StrikesScore").GetComponent<Text>();
        //}
       // strikesText.text = strikes.ToString();
        //hitsText.text = hits.ToString();
    }

    void Update()
    {
        strikesText.text = strikes.ToString();
        hitsText.text = hits.ToString();
    }

    /// <summary>
    /// Sets the stats to 0 and displays in-game
    /// </summary>
    //void Start () {

    //    hitsText = GameObject.Find("HitsScore").GetComponent<Text>();
    //    strikesText = GameObject.Find("StrikesScore").GetComponent<Text>();
    //    //hitsText.text = hits.ToString();
    //    //strikesText.text = strikes.ToString();
    //}
    /// <summary>
    /// Temporarily increments stat counts upon button press
    /// </summary>
    //void Update()
    //{
    //   // hitsText.text = hits.ToString();
    //   // strikesText.text = strikes.ToString();
    //}
    //    //Hits
    //    //if (Input.GetKeyDown(KeyCode.H))
    //    //{
    //    //    hits++;
    //        hitsText.text = hits.ToString();

    //   // }
    //    //Strikes
    //    //if (Input.GetKeyDown(KeyCode.S))
    //    //{
    //    //    strikes++;
    //        strikesText.text = strikes.ToString();
    //  //  }
    // //   if (strikes == 8 || strikes == 9)
    //  //  {
    ////        strikesText.color = Color.yellow;
    // //   }
    // //   else if (strikes >= 10)
    //  //  {
    // //       strikesText.color = Color.red;
    //  //  }
    //}


    public void EventBallNotHit()
    {
        strikes++;
        Debug.Log("strikes= " + strikes);
        strikesText.text = strikes.ToString();
        CheckStats();
    }
 
    public void EventBallHit()
    {
        hits++;
        Debug.Log("hits= " + hits);
        hitsText.text = hits.ToString();
        CheckStats();
    }
    public void EventFlagButton()
    {
       
        if (Ball.flagVis)
        {
            Ball.flagVis = false;
            
        }
        else if (!Ball.flagVis)
        {
            Ball.flagVis = true;
            
        }
    }
    /// <summary>
    /// Change the color when the number gets high enough
    /// </summary>
    private void CheckStats()
    {

        if (strikes == 8 || strikes == 9)
        {
            strikesText.color = Color.yellow;
        }
        else if (strikes >= 10)
        {
            strikesText.color = Color.red;
        }
    }
}
  

