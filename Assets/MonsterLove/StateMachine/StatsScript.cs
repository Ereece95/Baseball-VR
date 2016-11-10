using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;



public class Pitcher
    {
    //The first line in the read in file: Stat names
    string totalString = "";
    //The second line in the read in file: Stat nu,bers
    string totalString2 = "";
    StringReader read = new StringReader("MLB_Stats.csv");

    //Dictionary for the stats
    Dictionary<string, float> stats = new Dictionary<string, float>();


        public Pitcher()
        {

        //takes in the two lines
        totalString = read.ReadLine();
        totalString2 = read.ReadLine();

        int temp = totalString.Length;


        for(int i=0;i<temp; i++)
        {
            //gets the name of the stat and the value of that stat
            stats[getStatName()] =  getStat();
        }
        }
    //A method that gets a stat name
    string getStatName()
    {
        string statName = "";
        int i=0;
        while(true)
        {
            if(totalString.Substring(i,1)==",")
            {
                break;
            }
            statName = totalString.Substring(0,i);
            i++;
        }


        totalString.Substring(i, totalString.Length);

        return statName;
    }
    //A method that gets one stat value
    float getStat()
    {
        float num = 0;

        int i = 0;
        string temp = "";
        while (true)
        {
            
            if (totalString2.Substring(i, 1) == ",")
            {
                break;
            }
            temp= totalString2.Substring(0, i);
          

            i++;
        }
        float.TryParse(temp, out num);

        totalString.Substring(i, totalString.Length);

        return num;
    }

};



