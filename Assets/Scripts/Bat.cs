using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {

    public Transform cursorObject;
    public GameObject bat;
    private float depth = 1.5f;
    private GameController gc;

    // Use this for initialization
    void Start () {
        Cursor.visible = false;
        gc = GameObject.Find("GameController").GetComponent("GameController") as GameController;
    }
	
	// Update is called once per frame
	void Update () {
        if (gc.GetState() == States.ThrowPitch)
        {
            batFollowCursor();
        }
        else
        {
            //bat.enabled = false;
        }
	}

    void batFollowCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * depth);

        cursorObject.position = point;
    }
}
