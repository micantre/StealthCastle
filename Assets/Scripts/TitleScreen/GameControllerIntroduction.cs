using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerIntroduction : MonoBehaviour {

	public GameObject eventSystem;
	public GameObject title_panel;
	public GameObject controls_panel;

	public Animator anim;
	public Image black;

	/*Function to load next scene*/
	public void SceneLoader(int SceneIndex) {
		StartCoroutine(FadeScene(SceneIndex));
	}

	/*IEnumerator to stop for however long it takes to fade out*/
	IEnumerator FadeScene(int SceneIndex) {
		anim.SetBool("Fade", true);
		yield return new WaitUntil(()=>black.color.a==1);	//Wait until fading ends
		SceneManager.LoadScene (SceneIndex);
	}

	/*Function to change Panels to go to and from Controls Menu*/
	public void ChangePanels(bool BoolState) {
		if (BoolState == true) {
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem> ().SetSelectedGameObject(null);	//Destroy the currernt selected GameObject for eventsystem
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(controls_panel.transform.GetChild(1).gameObject);	//Set the new GameObject for eventsystem
			title_panel.SetActive(false);
			controls_panel.SetActive(true);
		} else {
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);	//Destroy the current selected GameObject for eventsystem
			eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(title_panel.transform.GetChild(0).gameObject);	//Set the new GameObject for eventsystem
			controls_panel.SetActive(false);
			title_panel.SetActive(true);
		}
	}
}
