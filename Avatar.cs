using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for The Dog Avatar
public class Avatar : MonoBehaviour
{
    //Speed of avatar animation
    static public float ImpulseForceMagnitude = 100.0f;
    
    //avatar held here
    static public GameObject avatar;

    //boolean used to see if avatar has reached the target tile
    static public bool onTarget = true;

    //Game Start
    void Start()
    {
        avatar = GameObject.Find("Dog");
    }

    // Update is called once per frame - 60 fps
    void Update()
    {
        //moved to path class
        //if(!onTarget)
        //{
            //Path.StopAtNextWayPoint(avatar);
        //}
    }
}
