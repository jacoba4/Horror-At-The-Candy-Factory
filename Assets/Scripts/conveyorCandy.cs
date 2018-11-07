using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorCandy : MonoBehaviour {

	[SerializeField]
	private Vector3 conveyorVec = Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
    {
		if (collision.gameObject.tag == "conveyor") {
			Vector3 dir = Vector3.right;
            dir = Quaternion.Euler(0, collision.transform.eulerAngles.y, 0) * dir;
            dir.Normalize();
            conveyorVec = dir;
		}
	}
}
