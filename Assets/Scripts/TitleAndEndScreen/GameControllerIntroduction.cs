using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameControllerIntroduction : MonoBehaviour {

	public GameObject eventSystem;
	public GameObject title_panel;
	public GameObject controls_panel;
	public Animator anim;
	public GameObject player;


	void Start() {
		StartCoroutine(Fading.instance.FadeToZeroAlpha());
		title_panel.SetActive(true);
		controls_panel.SetActive(false);
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);	//Destroy the current selected GameObject for eventsystem
		eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(title_panel.transform.GetChild(0).gameObject);	//Set the new GameObject for eventsystem
	}

	/*Function to load next scene*/
	public void SceneLoader(string SceneName) {
		anim.SetBool("StartGame", true);
		player.GetComponent<Animator>().SetInteger ("DIR", 0);
		player.GetComponent<Animator>().SetBool("IS_MOVING", true);
		StartCoroutine(Fading.instance.FadeToFullAlpha());
		StartCoroutine(Fading.instance.DoLast(SceneName));
	}

	/*Function to change Panels to go to and from Controls Menu*/
	public void ChangePanels(bool BoolState) {
		if (BoolState == true) {
			title_panel.SetActive(false);
			controls_panel.SetActive(true);
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);	//Destroy the currernt selected GameObject for eventsystem
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(controls_panel.transform.GetChild(1).gameObject);	//Set the new GameObject for eventsystem
		} else {
			controls_panel.SetActive(false);
			title_panel.SetActive(true);
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);	//Destroy the current selected GameObject for eventsystem
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(title_panel.transform.GetChild(0).gameObject);	//Set the new GameObject for eventsystem
		}
	}
}
