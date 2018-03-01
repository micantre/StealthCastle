﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableScript : MonoBehaviour {

	public float throwMultiplier = 1f;
	public float throwPower = 0f;
	public float maxPower = 17.5f;

	public Slider aimSlider;
	private RectTransform aimRectTransform;

	private GameObject throwable;
	private Rigidbody2D throwableRB2D;
	private CircleCollider2D throwableCollider2D;
	private ThrowableBehavior throwableBehaviorScript;

	private BoxCollider2D playerCollider2D;
	private Animator playerAnim;

	void Start() {
		playerAnim = GetComponent<Animator>();
		playerCollider2D = gameObject.GetComponent<BoxCollider2D>();

		aimRectTransform = aimSlider.GetComponent<RectTransform>();
		aimSlider.value = 0f;
		aimSlider.enabled = false;
	}

	void FixedUpdate() {
		if (GameController.instance.GetItemName().Equals("ThrowableCoin") ||
			GameController.instance.GetItemName().Equals("Caltrops")) {
			int dirInt = playerAnim.GetInteger("DIR");
			aimSlider.enabled = true;

			if (throwable == null) {
				throwable = GameController.instance.currItem;
				throwableRB2D = throwable.GetComponent<Rigidbody2D>();
				throwableCollider2D =
					throwable.GetComponent<CircleCollider2D>();
				throwableBehaviorScript =
					throwable.GetComponent<ThrowableBehavior>();
			}

			SetAimDirection(dirInt);
			if (Input.GetButton("UseItem")) {
				throwPower += 0.25f;
				aimSlider.value = throwPower;
			}
			if (Input.GetButtonUp("UseItem") || throwPower >= maxPower) {
				Vector2 throwVec = DirIntToVector(dirInt);
				SetAimDirection(dirInt);

				UseThrowable(throwVec, throwPower * throwMultiplier);
				throwPower = 0;
				aimSlider.value = throwPower;
			}
		}

		if (throwable != null && !throwableBehaviorScript.isBeingThrown) {
			Physics2D.IgnoreCollision(throwableCollider2D,
									  playerCollider2D,
									  false);
			throwable = null;
		}
	}

	private void SetAimDirection(int dirInt) {
		int UP = 0;
		int RIGHT = 1;
		int LEFT = 3;

		if (dirInt == UP) {
			aimRectTransform.localEulerAngles = new Vector3(0f, 0f, 0f);
		}
		else if (dirInt == RIGHT) {
			aimRectTransform.localEulerAngles = new Vector3(0f, 0f, -90f);
		}
		else if (dirInt == LEFT) {
			aimRectTransform.localEulerAngles = new Vector3(0f, 0f, 90f);
		}
		else {
			aimRectTransform.localEulerAngles = new Vector3(0f, 0f, 180f);
		}
	}

	private Vector2 DirIntToVector(int dirInt) {
		int UP = 0;
		int RIGHT = 1;
		int LEFT = 3;

		if (dirInt == UP) {
			return Vector2.up;
		}
		else if (dirInt == RIGHT) {
			return Vector2.right;
		}
		else if (dirInt == LEFT) {
			return Vector2.left;
		}
		else {
			return Vector2.down;
		}
	}

	private void UseThrowable(Vector2 dir, float power) {
		throwable.transform.position = this.transform.position;
		Physics2D.IgnoreCollision(throwableCollider2D, playerCollider2D, true);

		throwable.SetActive(true);
		throwableRB2D.AddForce(dir * power, ForceMode2D.Impulse);
		throwableBehaviorScript.isBeingThrown = true;
		GameController.instance.SetPlayerItem(null);
	}
}