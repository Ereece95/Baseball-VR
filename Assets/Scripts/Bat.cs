using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {

    public Transform cursorObject;
    public GameObject bat;
    private float depth = 1.5f;
    private GameController gc;

    // Use this for initialization
    void Start() {
        bat.GetComponent<Renderer>().enabled = false;
        Cursor.visible = true;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }
	
	// Update is called once per frame
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * depth);

        cursorObject.position = point;
    }
}
