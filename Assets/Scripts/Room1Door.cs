using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Door : MonoBehaviour {

	// Use this for initialization
	bool move = false;
	float t;
	float speed = 0.5f;
	void Start () {
		t = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(move)
		{
			
			transform.position = new Vector3(transform.position.x, (Mathf.Lerp(5,15, t)), transform.position.z);
			t += speed * Time.deltaTime;

			if(t > 1.0f)
			{
				move = false;
			}
		}
	}

	public void Act()
	{
			move = true;		
	}
}
