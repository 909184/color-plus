using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBehavior : MonoBehaviour {
	public int myX, myY;
	public bool active;
	GameController myGameController;

	void Start() {
		myGameController = GameObject.Find ("GameControllerObject").GetComponent<GameController> ();

	}



	void OnMouseDown() {
		myGameController.ProcessClick(gameObject, myX, myY, gameObject.GetComponent<Renderer>().material.color, active);

	}

}
