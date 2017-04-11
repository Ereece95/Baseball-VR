using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AdjHeight : MonoBehaviour
{

    int heightFeet;
    int heightInches;
    int heightType;

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
        switch (heightType)
        {
            case 0:
                changeHeightFeet();
                string height1 = heightFeet.ToString();
                feetDisplay.text = height1;
                break;
            case 1:
                changeHeightInches();
                string height2 = heightInches.ToString();
                inchesDisplay.text = height2;
                break;

        }
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
    void changeHeightFeet()
    {
        if (Input.GetButton("UpButton"))
        {
            heightFeet++;
        }
        else if (Input.GetButton("DownButton"))
        {
            heightFeet--;
        }
    }
    void changeHeightInches()
    {
        if (Input.GetButton("UpButton"))
        {
            heightInches++;
            if (heightInches > 11)
            {
                heightInches = 0;
                heightFeet++;
            }
        }
        else if (Input.GetButton("DownButton"))
        {
            heightInches--;
            if (heightInches < 0)
            {
                heightInches = 0;
                heightFeet--;
            }
        }
    }
    double convertHeight()
    {
        double newHeight = (heightFeet/3.28) + (heightInches * 0.0254);
        return newHeight;
    }
}