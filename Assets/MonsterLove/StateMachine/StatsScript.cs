using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;


public class StatsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
public class Pitcher
{
    StreamReader reader = new StreamReader(File.OpenRead("FileName.csv"));
    Dictionary<string, float> variables = new Dictionary<string, float>();

    public Pitcher()
    {
        while (!reader.EndOfStream)
        {
            string t = reader.ReadLine();
            variables[t] = float.MinValue;
        }
    }
    
    
};
