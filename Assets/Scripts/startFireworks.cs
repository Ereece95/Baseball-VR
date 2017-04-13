using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startFireworks : MonoBehaviour {

    public Transform firework;

	// Use this for initialization
	void Start () {
        firework.GetComponent<ParticleSystem> () .enableEmission = false; 
	}
    //void OnTriggerEnter(Collider fire)
    //{
        
    //}
    // Update is called once per frame
    void Update () {
		
	}
}
