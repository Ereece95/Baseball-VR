//Tyler Owens
//Will temporarily increment the in-game stat count.
//Will need to be integrated further when an actually hit is determined
//or if the pitch is a ball or strike when it passes the plate.
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateStats : MonoBehaviour {

  
    int hits, strikes = 0;
    [SerializeField]
    private Text hitsText, strikesText;

    
    /// <summary>
    /// Sets the stats to 0 and displays in-game
    /// </summary>
    void Start () {


        hitsText.text = hits.ToString();
        strikesText.text = strikes.ToString();
    }
	/// <summary>
    /// Temporarily increments stat counts upon button press
    /// </summary>
	//void Update () {
 //       //Hits
 //       if(Input.GetKeyDown(KeyCode.H))
 //       {
 //           hits++;
 //           hitsText.text = hits.ToString();

 //       }
 //       //Strikes
 //       if (Input.GetKeyDown(KeyCode.S))
 //       {
 //           strikes++;
 //           strikesText.text = strikes.ToString();
 //       }
 //       if(strikes == 8 || strikes == 9)
 //       {
 //           strikesText.color = Color.yellow;
 //       }else if(strikes >= 10)
 //       {
 //           strikesText.color = Color.red;
 //       }
 //   }
    public void IncrementStats(bool hit)
    {
        if (hit)
        {
            hits++;
            hitsText.text = hits.ToString();
        }
        else
        {
           strikes++;
           strikesText.text = strikes.ToString();

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
  
}
