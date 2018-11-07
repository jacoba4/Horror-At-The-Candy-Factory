using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorCandy : MonoBehaviour {

	[SerializeField]
	private Vector3 conveyorVec = Vector3.zero;
	[SerializeField]
	private float moveSpeed = 15;
	private int deleteAfter = 15;
	private float secondsAlive = 0;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(0,0,-6);
	}

	void Update() {
		secondsAlive += Time.deltaTime;
		if(secondsAlive > deleteAfter) {
			Destroy(this);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		
	}
}
