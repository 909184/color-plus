using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
	static float timer = 60.0f;
	public Text textBox;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		textBox.text = timer.ToString ("0.0");
		if (Time.time >= 50) {
			textBox.GetComponent<Text> ().color = Color.red;
		}
		if (Time.time >= 60) {
			textBox.text = ("0.0");
			GameController.EndGame (GameController.win);
		}
	}
}
