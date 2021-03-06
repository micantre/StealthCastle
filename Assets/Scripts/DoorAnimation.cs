﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Created By: Alex Hua
 * Purpose of the function is to open the door
 * or close it whenever an action triggeres it.*/
public class DoorAnimation : MonoBehaviour
{
    public static DoorAnimation instanceDoor;

    private Animator door_anim;
    private bool openDoor = false;
    private GameObject door;

    // Use this for initialization
    void Start()
    {
        door_anim = GetComponentInChildren<Animator>();
        door = GetComponent<GameObject>();
        instanceDoor = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*Open door condition*/
        if (openDoor == true)
        {
            door_anim.SetTrigger("DoorOpen");
            door_anim.ResetTrigger("DoorIdle");
            GetComponent<BoxCollider2D>().enabled = false;
        }
        /*Close door condition*/
        else
        {
            door_anim.SetTrigger("DoorIdle");
            door_anim.ResetTrigger("DoorOpen");
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    /*Function to toggle door status open or closed*/
    public void ChangeDoorStatus()
    {
        if (openDoor == false)
            openDoor = true;
        else
            openDoor = false;
    }
}    
