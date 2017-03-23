using UnityEngine;
using System.Collections;
 
 public class ChangeColorOfBox : MonoBehaviour
{
    GameObject rightbox;
    GameObject leftbox;

    void Start()
    {
        rightbox = GameObject.Find("RightBox");
        leftbox = GameObject.Find("LeftBox");
    }

    public void changeBatterBoxColor()
    {
        rightbox.SetActive(false);
        leftbox.SetActive(false);

    }
}

