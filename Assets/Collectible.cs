using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour{
    public GameObject eventObject;
    public GameObject eventManager;

    void Start()
    {
        eventManager = GameObject.FindGameObjectsWithTag("Event Manager")[0];
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            TEST();
        }
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

    void TEST()
    {
        eventObject.GetComponent<EventObject>().Triggered();
        eventManager.GetComponent<EventManager>().RemoveCollectible(gameObject);
        Destroy(gameObject);
    }

    

}