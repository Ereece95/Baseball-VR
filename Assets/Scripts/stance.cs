using UnityEngine;
using System.Collections;

public class stance : MonoBehaviour {
      
    private GameObject headset;
    private GameObject rightBox;
    private GameObject leftBox;
    
    // Use this for initialization
    void Start () {

        headset = GameObject.FindWithTag("MainCamera");
        rightBox = GameObject.Find("RightyBox");
        leftBox = GameObject.Find("LeftyBox");

    }
    
    void changecolor()
    {
    
        if (headset.transform.position != leftBox.transform.position || headset.transform.position != rightBox.transform.position)
        {
         
        }

    }
   // looking from catchers view
   // topleftR =  x:-.86 z:1.67  toprightR= x:-.14 z:.95  botleftR = x:-2.04  z:.54  botrightR = x:-1.28  z:-.21
   // toprightL = x: 1.66  z:-.87  topleftL= x: .95  z:-.14  botleftL= x:-.2  z:-1.29  botrightL= x:.5  z:-2.03
}
