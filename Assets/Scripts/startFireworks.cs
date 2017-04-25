using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class startFireworks : MonoBehaviour
{

    public ParticleSystem.EmissionModule fire;
    private GameController gc;
    bool homerun; // homerun has occured

    void Start()
    {
        // find firewroks and disable on start
        fire = GameObject.Find("fireworks").GetComponent<ParticleSystem>().emission;
        fire.enabled = false;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
        homerun = true;
    }

    /// <summary>
    /// Displays what pitch was thrown with abutton click
    /// </summary>
    void Update()
    {
        //if homerun it will display homerun
        if (homerun && (gc.GetState() == States.WaitForInput))
        {
            fire.enabled = true;


        }
        // otherwise reenable pitch type
        else
        {
            fire.enabled = false;
            homerun = false;
        }
    }
    public void fireStart()
    {
        homerun = true;
    }

}
