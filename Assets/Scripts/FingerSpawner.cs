using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSpawner : MonoBehaviour {

	int framecount = 0;
	int framemax = 30;
	public GameObject finger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(framecount >= framemax)
		{
			Instantiate(finger,transform.position,transform.rotation);
			framecount = 0;
		}
		framecount++;
	}
}
