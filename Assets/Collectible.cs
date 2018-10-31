using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour{
    public GameObject eventObject;
    public GameObject eventManager;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            eventObject.GetComponent<EventObject>().Triggered();
            eventManager.GetComponent<EventManager>().RemoveCollectible(gameObject);
            Destroy(gameObject);
        }
    }

    

}