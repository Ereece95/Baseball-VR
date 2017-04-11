using UnityEngine;
using System.Collections;

/// <summary>
/// To send and choose the pitch types and the qudrant where the ball goes
/// </summary>
public class StatsState : MonoBehaviour
{
    public static int pitch;
    public static int qaudrent;
    public StatsScript s;
    public string[] index;

    // Use this for initialization
    /// <summary>
    /// Get everything from the stats list and use them
    /// </summary>
    void Start ()
    {
       s= new StatsScript();

        
        index = new string[13];

        index[0] = "Arrieta, J.";
        
        for(int i=1;i<13;i++)
        {
            index[i] = i.ToString();
        }

        pitch = getPitchType();
        Debug.Log(pitch);
        qaudrent=setQuadrent();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
    /// <summary>
    /// Compares the information from the stats and chooses a pitch type based on them
    /// </summary>
    /// <returns></returns>
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
            return 0;
        }
        else if(rand <=cbp)
        {
            return 1;
        }
        else if(rand<=slp)
        {
            return 2;
        }
        else if(rand<=fbp)
        {
            return 3;
        }
        else 
        {
            return 4;
        }

    }
    /// <summary>
    /// Based on stats makes the ball go to a certain quadrant in the strike zone
    /// </summary>
    /// <param name="p">Used later to get the index from the stats</param>
    /// <param name="t">used later to say what type of pitch will be thrown</param>
    /// <returns></returns>
    public  double getQudrent(string p, int t)
    {
        double percent = -1;

        if(t==0)
        {
            double.TryParse(s.Find_Player(index[0]).TotalChangeups, out percent);
        }
        else if(t==1)
        {
            double.TryParse(s.Find_Player(index[0]).TotalCurveballs, out percent);
        }
        else if (t == 2)
        {
            double.TryParse(s.Find_Player(index[0]).TotalSliders, out percent);
        }
        else if (t == 3)
        {
            double.TryParse(s.Find_Player(index[0]).TotalFastballs, out percent);
        }
        else
        {
            double.TryParse(s.Find_Player(index[0]).TotalSinkers, out percent);
        }
        return percent;
    }
    /// <summary>
    /// Increments through the stats and decides where to go for quadrants 1-13
    /// for now if it goes above 9 it goes again since every throw is a strike
    /// </summary>
    /// <returns>returns -1 if something goes wrong</returns>
    public int setQuadrent()
    {
        double[] quad = new double[13];
        for(int i=0;i<13;i++)
        {
            if (i != 0)
            {
                quad[i] = quad[i - 1] + getQudrent(index[i], pitch);
            }
            else
            {
                quad[i]= getQudrent(index[i], pitch);
            }
        }
        int rand = Random.Range(0, 99);
        for (int i = 0; i < 13; i++)
        {
            if (quad[i] >= rand)
            {
                return i;
            }
        }
        return -1;
    }
}
