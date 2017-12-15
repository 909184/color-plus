using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour {
	public Text textBox;

	// Use this for initialization
	void Start () {
		textBox.text = ("");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= GameController.gameTime) {
			textBox.text = ("game over!" +
			"score: " + GameController.score);
		}
	}
}

