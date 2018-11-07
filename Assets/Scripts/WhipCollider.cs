using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipCollider : MonoBehaviour {

    public GameObject player;
    private PlayerController pc;
	// Use this for initialization
	void Start () {
        pc = player.GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (pc.isWhipping && other.gameObject.tag == "Bottle")
        {
            Destroy(other.gameObject);
        }
    }
}
