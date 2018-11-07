using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour{
    public GameObject eventObject;
    public GameObject eventManager;

    float rotatespeed = 1.5f;
    bool fly = false;
    public float speed = 14;

    void Start()
    {
        eventManager = GameObject.FindGameObjectsWithTag("Event Manager")[0];
    }

    void Update()
    {
        if(fly)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, eventObject.transform.position, step);
            if(transform.position == eventObject.transform.position)
            {
                eventObject.transform.SendMessage("Act");
                eventManager.GetComponent<EventManager>().RemoveCollectible(gameObject);
                Destroy(gameObject);
            }
        }
        transform.Rotate(rotatespeed,0f,0f);
        transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time)/100, transform.position.z);
    }

    void Collided()
    {
        fly = true;
        
    }

    

}