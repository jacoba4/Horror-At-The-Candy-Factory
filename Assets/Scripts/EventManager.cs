using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour{

    public GameObject[] collectibles;

    void Start()
    {
        //Getting all the objects
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Collectible");
        collectibles = new GameObject[temp.Length];
        for(int i = 0; i < temp.Length; i++)
        {
            collectibles[i] = temp[i];
        }

    }

    void Update()
    {

    }

    public void RemoveCollectible(GameObject rem)
    {
        //First make sure the GO is in the array
        bool inArray = false;
        for(int i = 0; i < collectibles.Length-1; i++)
        {
            if(collectibles[i] == rem)
            {
                inArray = true;
                break;
            } 
        }

        if(!inArray)
        {
            return;
        }


        //If the object is in the array
        GameObject[] temp = new GameObject[collectibles.Length-1];
        int offset = 0;
        for(int i = 0; i < collectibles.Length-1; i++)
        {
            if(collectibles[i] != rem)
            {
                temp[i - offset] = collectibles[i];
            }
            else
            {
                offset = 1;
            }
        }

        collectibles = temp;
    }
}