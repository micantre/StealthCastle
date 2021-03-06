﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Written by Daniel Anderson
/// </summary>
public class PickUpController : MonoBehaviour, IRespawnable {

    public string displayName;
	public bool itemIsDisguise = false;

    private GameObject indicator;
	private Rigidbody2D rb2d;
	private BoxCollider2D boxCol2d;

    //Respawnable
    private Vector3 spawnPosition;
    private bool isActiveOnSpawn;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		boxCol2d = GetComponent<BoxCollider2D>();
        indicator = transform.GetChild(0).gameObject;

        //Respawnable
        spawnPosition = transform.position;
        isActiveOnSpawn = gameObject.activeSelf;
	}

    public void pickupReady(bool state)
    {
        indicator.SetActive(state);
    }

	public void DropItem(Vector3 position) {
		gameObject.transform.position = position;
	}

	/* Created by Mitchell Keller
	 * Checks triggers to see if player 
	 * has picked up a gem.
	 */

    private void OnTriggerStay2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" &&
			 displayName.Equals("Gem")) {
            gameObject.SetActive(false);
            isActiveOnSpawn = false;
        }
	}

    public string GetName() { return displayName; }


    /* Respawn
    * Created by Michael Cantrell
    * Resets this class's attributes to their original states
    */
    public void Respawn()
    {
        transform.position = spawnPosition;
        gameObject.SetActive(isActiveOnSpawn);
        indicator.SetActive(false);
    }
}
