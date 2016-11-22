using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hit : MonoBehaviour {

    private int distance;
    private bool isFoul;
    private bool isHomerun;
    private string pitchHit;

    public Hit(int distance, bool isFoul, bool isHomerun, string typePitch)
    {
        this.distance = distance;
        this.isFoul = isFoul;
        this.isHomerun = isHomerun;
        this.pitchHit = typePitch;
    }

  




   public static void main()
    {
        Stack<Hit> hitsStats = new Stack<Hit>();

    }



    // Use this for initialization
    void Start () {
      

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
