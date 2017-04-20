﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AdjHeight : MonoBehaviour
{

    int heightFeet;
    int heightInches;
    int heightType;
    public bool feetInches;

    public Text feetDisplay;
    public Text inchesDisplay;


    // Use this for initialization
    void Start()
    {
        feetDisplay = GameObject.Find("heightFeetText").GetComponent<Text>();
        inchesDisplay = GameObject.Find("heightInchText").GetComponent<Text>();

        //heightType = 0;
        heightFeet = 5;
        heightInches = 8;

        feetDisplay.text = heightFeet.ToString();
        inchesDisplay.text = heightInches.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //heightType = getType();
        feetDisplay.text = heightFeet.ToString();

        inchesDisplay.text = heightInches.ToString();
    }
    //int getType()
    //{
    //    int type;
    //    if (Input.GetButton("toFeetButton"))
    //    {
    //        type = 0;
    //    }
    //    else
    //    {
    //        type = 1;
    //    }
    //    return type;
    //}
    public void changeHeightFeet(bool heightType)
    {
        Debug.Log("Change feet");
        if (heightType == true)
        {
            Debug.Log("feet up");
            heightFeet = heightFeet + 1;
        }
        else if (heightType == false)
        {
            Debug.Log("feet down");
            heightFeet = heightFeet - 1;
        }
    }
    public void changeHeightInches(bool heightType)
    {
        Debug.Log("Change inches");
        if (heightType)
        {
            Debug.Log("inches up");
            heightInches = heightInches + 1;
            if (heightInches > 11)
            {
                heightInches = 0;
                heightFeet = heightFeet + 1;
            }
        }
        else
        {
            Debug.Log("inches down");
            heightInches = heightInches - 1;
            if (heightInches < 0)
            {
                heightInches =11;
                heightFeet = heightFeet - 1;
            }
        }
    }
    public void switchHeightType(bool type)
    {
        Debug.Log("Change feet/inches type");
        feetInches = type;
    }
    double convertHeight()
    {
        double newHeight = (heightFeet / 3.28) + (heightInches * 0.0254);
        return newHeight;
    }
}