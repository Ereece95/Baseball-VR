using UnityEngine;
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

        heightType = 0;
        heightFeet = 5;
        heightInches = 8;

        feetDisplay.text = heightFeet.ToString();
        inchesDisplay.text = heightInches.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        heightType = getType();

        string height1 = heightFeet.ToString();
        feetDisplay.text = height1;

        string height2 = heightInches.ToString();
        inchesDisplay.text = height2;
    }
    int getType()
    {
        int type;
        if (Input.GetButton("toFeetButton"))
        {
            type = 0;
        }
        else
        {
            type = 1;
        }
        return type;
    }
    public void changeHeightFeet(bool heightType)
    {
        if (heightType == true)
        {
            heightFeet++;
        }
        else if (heightType == false)
        {
            heightFeet--;
        }
    }
    public void changeHeightInches(bool heightType)
    {
        if (heightType)
        {
            heightInches++;
            if (heightInches > 11)
            {
                heightInches = 0;
                heightFeet++;
            }
        }
        else
        {
            heightInches--;
            if (heightInches < 0)
            {
                heightInches = 0;
                heightFeet--;
            }
        }
    }
    public void switchHeightType(bool type)
    {
        feetInches = type;
    }
    double convertHeight()
    {
        double newHeight = (heightFeet / 3.28) + (heightInches * 0.0254);
        return newHeight;
    }
}