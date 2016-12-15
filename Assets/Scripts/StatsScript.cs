using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The file with all the statistics from a pitcher is loaded and placed in vaiables
/// </summary>
public class StatsScript : MonoBehaviour
{

    public TextAsset file;
    public static int pitch;
    public static int qaudrent;
    public string[] index;
    

    /// <summary>
    /// Loads the file where the stats are stored
    /// </summary>
    void Start()
    {
        Load(file);
       


        index = new string[13];

        index[0] = "Arrieta, J.";

        for (int i = 1; i < 13; i++)
        {
            index[i] = i.ToString();
        }

        pitch = getPitchType();
        Debug.Log(pitch);
        qaudrent = setQuadrent();

    }
    public class Row
    {
        public string Player;
        public string TotalPitchesThrown;
        public string TotalFastballs;
        public string TotalCurveballs;
        public string TotalChangeups;
        public string TotalSliders;
        public string TotalSinkers;
        public string FB_R;
        public string FB_L;
        public string CV_R;
        public string CV_L;
        public string CH_R;
        public string CH_L;
        public string SL_R;
        public string SL_L;
        public string SI_R;
        public string SI_L;

    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.Player = grid[i][0];
            row.TotalPitchesThrown = grid[i][1];
            row.TotalFastballs = grid[i][2];
            row.TotalCurveballs = grid[i][3];
            row.TotalChangeups = grid[i][4];
            row.TotalSliders = grid[i][5];
            row.TotalSinkers = grid[i][6];
            row.FB_R = grid[i][7];
            row.FB_L = grid[i][8];
            row.CV_R = grid[i][9];
            row.CV_L = grid[i][10];
            row.CH_R = grid[i][11];
            row.CH_L = grid[i][12];
            row.SL_R = grid[i][13];
            row.SL_L = grid[i][14];
            row.SI_R = grid[i][15];
            row.SI_L = grid[i][16];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_Player(string find)
    {
        return rowList.Find(x => x.Player == find);
    }
    public List<Row> FindAll_Player(string find)
    {
        return rowList.FindAll(x => x.Player == find);
    }
    public Row Find_TotalPitchesThrown(string find)
    {
        return rowList.Find(x => x.TotalPitchesThrown == find);
    }
    public List<Row> FindAll_TotalPitchesThrown(string find)
    {
        return rowList.FindAll(x => x.TotalPitchesThrown == find);
    }
    public Row Find_TotalFastballs(string find)
    {
        return rowList.Find(x => x.TotalFastballs == find);
    }
    public List<Row> FindAll_TotalFastballs(string find)
    {
        return rowList.FindAll(x => x.TotalFastballs == find);
    }
    public Row Find_TotalCurveballs(string find)
    {
        return rowList.Find(x => x.TotalCurveballs == find);
    }
    public List<Row> FindAll_TotalCurveballs(string find)
    {
        return rowList.FindAll(x => x.TotalCurveballs == find);
    }
    public Row Find_TotalChangeups(string find)
    {
        return rowList.Find(x => x.TotalChangeups == find);
    }
    public List<Row> FindAll_TotalChangeups(string find)
    {
        return rowList.FindAll(x => x.TotalChangeups == find);
    }
    public Row Find_TotalSliders(string find)
    {
        return rowList.Find(x => x.TotalSliders == find);
    }
    public List<Row> FindAll_TotalSliders(string find)
    {
        return rowList.FindAll(x => x.TotalSliders == find);
    }
    public Row Find_TotalSinkers(string find)
    {
        return rowList.Find(x => x.TotalSinkers == find);
    }
    public List<Row> FindAll_TotalSinkers(string find)
    {
        return rowList.FindAll(x => x.TotalSinkers == find);
    }
    public Row Find_FB_R(string find)
    {
        return rowList.Find(x => x.FB_R == find);
    }
    public List<Row> FindAll_FB_R(string find)
    {
        return rowList.FindAll(x => x.FB_R == find);
    }
    public Row Find_FB_L(string find)
    {
        return rowList.Find(x => x.FB_L == find);
    }
    public List<Row> FindAll_FB_L(string find)
    {
        return rowList.FindAll(x => x.FB_L == find);
    }
    public Row Find_CV_R(string find)
    {
        return rowList.Find(x => x.CV_R == find);
    }
    public List<Row> FindAll_CV_R(string find)
    {
        return rowList.FindAll(x => x.CV_R == find);
    }
    public Row Find_CV_L(string find)
    {
        return rowList.Find(x => x.CV_L == find);
    }
    public List<Row> FindAll_CV_L(string find)
    {
        return rowList.FindAll(x => x.CV_L == find);
    }
    public Row Find_CH_R(string find)
    {
        return rowList.Find(x => x.CH_R == find);
    }
    public List<Row> FindAll_CH_R(string find)
    {
        return rowList.FindAll(x => x.CH_R == find);
    }
    public Row Find_CH_L(string find)
    {
        return rowList.Find(x => x.CH_L == find);
    }
    public List<Row> FindAll_CH_L(string find)
    {
        return rowList.FindAll(x => x.CH_L == find);
    }
    public Row Find_SL_R(string find)
    {
        return rowList.Find(x => x.SL_R == find);
    }
    public List<Row> FindAll_SL_R(string find)
    {
        return rowList.FindAll(x => x.SL_R == find);
    }
    public Row Find_SL_L(string find)
    {
        return rowList.Find(x => x.SL_L == find);
    }
    public List<Row> FindAll_SL_L(string find)
    {
        return rowList.FindAll(x => x.SL_L == find);
    }
    public Row Find_SI_R(string find)
    {
        return rowList.Find(x => x.SI_R == find);
    }
    public List<Row> FindAll_SI_R(string find)
    {
        return rowList.FindAll(x => x.SI_R == find);
    }
    public Row Find_SI_L(string find)
    {
        return rowList.Find(x => x.SI_L == find);
    }
    public List<Row> FindAll_SI_L(string find)
    {
        return rowList.FindAll(x => x.SI_L == find);
    }


    public double getFBStatR(string p)
    {
        string sample = (Find_Player(p).FB_R);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getFBStatL(string p)
    {
        string sample = (Find_Player(p).FB_L);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }
    public double getCVStatR(string p)
    {
        string sample = (Find_Player(p).CV_R);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getCVStatL(string p)
    {
        string sample = (Find_Player(p).CV_L);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }
    public double getCHStatR(string p)
    {
        string sample = (Find_Player(p).CH_R);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getCHStatL(string p)
    {
        string sample = (Find_Player(p).CH_L);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getSLStatR(string p)
    {
        string sample = (Find_Player(p).SL_R);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getSLStatL(string p)
    {
        string sample = (Find_Player(p).SL_L);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getSIStatR(string p)
    {
        string sample = (Find_Player(p).SI_R);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public double getSIStatL(string p)
    {
        string sample = (Find_Player(p).SI_L);
        double temp = 0;

        sample = sample.Substring(0, sample.Length - 1);
        double.TryParse(sample, out temp);
        return temp;
    }

    public int getPitchType()
    {
        int fbp;
        int.TryParse(Find_Player(index[0]).TotalFastballs, out fbp);

        int cbp;
        int.TryParse(Find_Player(index[0]).TotalCurveballs, out cbp);

        int chp;
        int.TryParse(Find_Player(index[0]).TotalChangeups, out chp);

        int slp;
        int.TryParse(Find_Player(index[0]).TotalSliders, out slp);

        int sip;
        int.TryParse(Find_Player(index[0]).TotalSinkers, out sip);

        int rand = Random.Range(1, 3115);

        if (rand <= chp)
        {
            return 0;
        }
        else if (rand <= cbp)
        {
            return 1;
        }
        else if (rand <= slp)
        {
            return 2;
        }
        else if (rand <= fbp)
        {
            return 3;
        }
        else
        {
            return 4;
        }

    }
    public double getQudrent(string p, int t)
    {
        double percent = -1;

        if (t == 0)
        {
            percent = getCHStatR(p);   
        }
        else if (t == 1)
        {
            percent = getCVStatR(p); 
        }
        else if (t == 2)
        {
            percent = getSLStatR(p); 
        }
        else if (t == 3)
        {
            percent = getFBStatR(p);
        }
        else
        {
            percent = getSIStatR(p);
        }
        return percent;
    }
    public int setQuadrent()
    {
        double[] quad = new double[13];
        for (int i = 0; i < 13; i++)
        {
            if (i != 0)
            {
                quad[i] = quad[i - 1] + getQudrent(index[i], pitch);
            }
            else
            {
                quad[i] = getQudrent(index[i], pitch);
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