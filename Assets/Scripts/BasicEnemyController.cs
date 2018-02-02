﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BasicEnemyController : MonoBehaviour {

    //state machine states
    public static readonly int STATE_PATHING = 0, STATE_ALERT = 1, STATE_HUNTING = 2;


    //pathfinding controller
    public GameObject nextNode;

    private GameObject visionCone;
    private GameObject lastNode;
    private AIPath pathController;

    private Animator animationController;

    private int state;

    public void Start()
    {
        visionCone = transform.GetChild(0).gameObject;
        animationController = this.GetComponent<Animator>();
        pathController = this.GetComponent<AIPath>();
        //move to self, to kick off pathfinding
        pathController.destination = nextNode.transform.position;
        pathController.SearchPath();
        state = BasicEnemyController.STATE_PATHING;
    }

    public void Update()
    {
        if(!pathController.pathPending && pathController.reachedEndOfPath)
        {
            ArrivedAtDestination();
        }
        SetDir();
    }

    private void ArrivedAtDestination()
    {
        lastNode = nextNode;
        if (state == BasicEnemyController.STATE_PATHING)
        {
            nextNode = lastNode.GetComponent<PathNodeController>().getNextNode();
            UpdateDestination(nextNode.transform.position);
        }
        else if (state == BasicEnemyController.STATE_HUNTING)
        {
            state = STATE_PATHING;
            pathController.maxSpeed = 2;
            UpdateDestination(lastNode.transform.position);
        }
    }

    //update destination based on current state
    private void UpdateDestination(Vector3 newDestination)
    {
        visionCone.SendMessage("rotateVision", newDestination);
        pathController.destination = newDestination;
        pathController.SearchPath();

    }

    //called when a player is in direct LOS
    public void PlayerInVision(GameObject player)
    {
        state = BasicEnemyController.STATE_HUNTING;
        pathController.maxSpeed = 4;
        UpdateDestination(player.transform.position);
    }


    private void SetDir()
    {
        float horizontal = pathController.velocity.x, vertical = pathController.velocity.y;
        if (horizontal == 0 && vertical == 0)
        {
            animationController.SetBool("IS_MOVING", false);
            return;
        }
        if (horizontal >= vertical)
        {
            if (horizontal > 0)
            {
                animationController.SetInteger("DIR", 1);//right
            }
            else
            {
                animationController.SetInteger("DIR", 2);//left
            }
        }
        else
        {
            if (vertical > 0)
            {
                animationController.SetInteger("DIR", 0);//up
            }
            else
            {
                animationController.SetInteger("DIR", 3);//down
            }
        }
        animationController.SetBool("IS_MOVING", true);
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "SoundRing") { 
			//the guard just heard the player
			state = BasicEnemyController.STATE_HUNTING;
			pathController.maxSpeed = 4;
			UpdateDestination(other.transform.position);
		}
	}
}
