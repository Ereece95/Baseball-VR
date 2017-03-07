using UnityEngine;
using System.Collections;
using UnityEngine.VR;


/// <summary>
/// Switching between the cursor and the bat 
/// </summary>
public class Bat : MonoBehaviour {

    public Transform cursorObject;
    public GameObject bat;
    public GameObject cubeRight;
    public GameObject cubeLeft; 
    private float depth = 1.5f;
    private GameController gc;

    // Use this for initialization
    /// <summary>
    /// Get the bat and set the cursor to true for the main menu
    /// </summary>
    void Start() {
        bat.GetComponent<Renderer>().enabled = false;
        Cursor.visible = true;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }
	
	/// <summary>
    /// Sets the cursor true or false and whether the bat is true or false bassed on the state
    /// </summary>
	void Update () {

        if (gc.GetState() == States.ThrowPitch || gc.GetState() == States.ThrowPitchDone)
        {
            Cursor.visible = false;
            bat.GetComponent<Renderer>().enabled = true;
            batFollowCursor();
        }
        else if (gc.GetState() == States.WaitForInput || gc.GetState() == States.WaitForCollision)
        {
            Cursor.visible = true;
            bat.GetComponent<Renderer>().enabled = false;
        }
	}

    void batFollowCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(cursorObject.position);
        Vector3 point = ray.origin + (ray.direction * depth);

        cursorObject.position = point;      
        
    }

    void getStanceStraight(Collision collision)
    {
        //if(gc.GetState() == States.Orientation)
    
       //if(InputTracking.GetLocalPosition(VRNode.Head) == GameObject.FindObjectOfType<>_)

        
        
    }
}
