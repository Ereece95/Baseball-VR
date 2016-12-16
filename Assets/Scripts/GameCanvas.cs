using UnityEngine;
using System.Collections;

public class GameCanvas : MonoBehaviour {

    public CanvasGroup gameCanvas;

    private void Awake()
    {
        gameCanvas = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
    }

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //public void enableGameCanvasGroup(bool no);
    //{
    //    if (no)
    //    {
    //        gameCanvas.interactable = false;
    //        gameCanvas.alpha = 0;
    //        gameCanvas.blocksRaycasts = false;
    //    }  
    //    else
    //    {
    //        gameCanvas.interactable = true;
    //        gameCanvas.alpha = 0;
    //        gameCanvas.blocksRaycasts = true;
    //    } 
    //}
}
