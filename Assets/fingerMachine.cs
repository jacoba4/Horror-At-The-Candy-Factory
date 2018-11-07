using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerMachine : MonoBehaviour {

	[SerializeField]
	private int machineHealth = 1;

	void Update() {
		if (machineHealth == 0) {
			//end of game
			SystemManager.LoadGivenScene("GameOver");
		}
	}
	
	void OnCollisionEnter(Collision collision)
    { 
		if(collision.gameObject.tag == "Player") {
			machineHealth--;
		}
	}

}
