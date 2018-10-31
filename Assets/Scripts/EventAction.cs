using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAction : MonoBehaviour {

	// Use this for initialization
	bool move = false;
	float t;
	public float speed;
	void Start () {
		t = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(move)
		{
			
			transform.position = new Vector3(0, (Mathf.Lerp(5,15, t)), -15);
			t += speed * Time.deltaTime;

			if(t > 1.0f)
			{
				move = false;
			}
		}
	}

	public void Act()
	{
		if(gameObject.name == "Room 1 Door")
		{
			move = true;
		}

		
	}
}
