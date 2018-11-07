using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerSpawner : MonoBehaviour {

	int framecount = 0;
	int framemax = 30;
	public GameObject finger;

	[SerializeField]
	private FingerSpawner pairSpawner;
	
	private bool isActive = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(framecount >= framemax && isActive)
		{
			Instantiate(finger,transform.position,transform.rotation);
			framecount = 0;
		}
		framecount++;
	}

	public void Act() {
		isActive = true;
		pairSpawner.Act();
	}
}
