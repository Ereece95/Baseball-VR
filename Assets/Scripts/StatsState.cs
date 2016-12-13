using UnityEngine;
using System.Collections;

public class StatsState : MonoBehaviour
{
    public static int pitch = -1;
    public StatsScript s;
    public string[] index;

    // Use this for initialization
    void Start ()
    {
       s= new StatsScript();

        
        index = new string[13];

        index[0] = "Arrieta, J.";
        
        for(int i=1;i<13;i++)
        {
            index[i] = i.ToString();
        }



    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    
    public int getPitchType()
    {
        int fbp;
        int.TryParse(s.Find_Player(index[0]).TotalFastballs, out fbp);

        int cbp;
        int.TryParse(s.Find_Player(index[0]).TotalCurveballs, out cbp);

        int chp;
        int.TryParse(s.Find_Player(index[0]).TotalChangeups, out chp);

        int slp;
        int.TryParse(s.Find_Player(index[0]).TotalSliders, out slp);

        int sip;
        int.TryParse(s.Find_Player(index[0]).TotalSinkers, out sip);

        int rand = Random.Range(1, 3115);

        if(rand<=chp)
        {
            return rand;
        }
        else if(rand <=cbp)
        {
            return rand;
        }
        else if(rand<=slp)
        {
            return rand;
        }
        else if(rand<=fbp)
        {
            return rand;
        }
        else 
        {
            return rand;
        }

    }
    public void getQudrent(string p)
    {

    }
}
