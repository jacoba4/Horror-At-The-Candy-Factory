using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObject : MonoBehaviour{
    public EventAction eventAction;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Triggered()
    {
        eventAction.Act();
    }
}