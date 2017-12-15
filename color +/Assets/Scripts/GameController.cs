using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	int firstTurn;
	int turnLength;
	int gameTime;
	int gridX, gridY;
	public int x, y;

	int score;
	int rainbowValue;
	int samecolorValue;


	Vector3 cubePosition;
	Vector3 nextCubePosition;

	public GameObject cubePrefab;
	GameObject[,] grid;
	GameObject nextCube;
	GameObject activeCube;
	public GameObject clickedCube;

	Color[] myColors = {Color.blue, Color.green, Color.red, Color.yellow, Color.magenta};


	bool nextCubeFull;
	public static bool win;


	// Use this for initialization
	void Start () {
		gameTime = 60;
		firstTurn = 2;
		turnLength = 2;
		gridX = 8;
		gridY = 5;

		score = 0;
		rainbowValue = 5;
		samecolorValue = 10;

		activeCube = null;


		nextCubePosition = new Vector3 (0, 5, 0);
		nextCube = Instantiate (cubePrefab, nextCubePosition, Quaternion.identity);
		nextCube.GetComponent<Renderer> ().material.color = myColors[Random.Range (0, myColors.Length)];
		nextCubeFull = true;


		grid = new GameObject[gridX, gridY];

		for (int y = 0; y < gridY; y++){
			for (int x = 0; x < gridX; x++){
				//instantiate 8x5 grid of white cubes
				cubePosition = new Vector3 (x*2-7, y*2-5, 0);
				grid [x,y] = Instantiate (cubePrefab, cubePosition, Quaternion.identity);
				grid [x,y].GetComponent<Renderer>().material.color = Color.white;
			}
		}
	}

	int FindAvailableCube (int y){
		//takes input row, finds an empty white cube, returns x
		// if there are none, returns -1

		//placeholder
		return Random.Range (0, gridX);
	}

	public static void EndGame (bool win){
		//ends game
		if (win) {
			print ("game over! you win!");
			//win. i want a new screen so placeholder
		} 
		else {
			print ("game over! try again :(");
			//lose
		}
	}

	void PlaceNextCube (int y){
		int x = FindAvailableCube (y);
		//no available cube in chosen row
		if (x == -1) {
			EndGame (false);
		} 
		else {
			grid [x, y].GetComponent<Renderer> ().material.color = nextCube.GetComponent<Renderer> ().material.color;
			nextCubeFull = false;
		}
		if (nextCubeFull == false){
			nextCube.GetComponent<Renderer> ().material.color = Color.gray;
		}
	}

	void ProcessKeyboard (){
		int keyPressed = 0;
		if (Input.GetKey (KeyCode.Alpha1) || Input.GetKey (KeyCode.Keypad1)) {
			keyPressed = 1;
		}
		if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)){
			keyPressed = 2;
		}
		if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)){
			keyPressed = 3;
		}
		if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4)){
			keyPressed = 4;
		}
		if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)){
			keyPressed = 5;
		}
		//looks for available cube in row. -1 bc our rows start w 0
		FindAvailableCube (keyPressed - 1);
		if (keyPressed != 0 && nextCubeFull){
			PlaceNextCube (keyPressed - 1);
			nextCubeFull = false;
		}
	}

	bool IsRainbowPlus (int x, int y){
		Color a = grid [x, y].GetComponent<Renderer> ().material.color;
		Color b = grid [x, y].GetComponent<Renderer> ().material.color;
		Color c = grid [x, y].GetComponent<Renderer> ().material.color;
		Color d = grid [x, y].GetComponent<Renderer> ().material.color;
		Color e = grid [x, y].GetComponent<Renderer> ().material.color;

		if (a == Color.white || a == Color.black ||
		    b == Color.white || b == Color.black ||
		    c == Color.white || c == Color.black ||
		    d == Color.white || d == Color.black ||
		    e == Color.white || e == Color.black) {
			return false;
		} 
		if (a != b && a != c && a != d && a != e &&
		    b != c && b != d && b != e &&
		    c != d && c != e &&
		    d != e) {
			return true;
		}
		else {
			return false;
		}
	}
		
//	bool IsSameColorPlus (int x, int y){
//
//	}

	void BlackOutPlus (int x, int y){
		grid [x,y].GetComponent<Renderer>().material.color = Color.black;
		grid [x-1,y].GetComponent<Renderer>().material.color = Color.black;
		grid [x+1,y].GetComponent<Renderer>().material.color = Color.black;
		grid [x,y-1].GetComponent<Renderer>().material.color = Color.black;
		grid [x,y+1].GetComponent<Renderer>().material.color = Color.black;
	}

	public void ProcessClick(GameObject clickedCube, int x, int y, Color cubeColor, bool active) {
//		click on white/black, nothing happens.
		//is there no active cube
		print ("x: " + x + " y: " + y);
		if (cubeColor != Color.black && cubeColor != Color.white) {

			//if colored clicked cube is active,
			if (active) {
				//deactivate
				clickedCube.transform.localScale /= 1.5f;
				clickedCube.GetComponent<ClickBehavior> ().active = false;
				activeCube = null;
			}
			//if colored cube wasnt active
			else {
				//deactivate prev active cubes	
				if (activeCube != null) {
					activeCube.transform.localScale /= 1.5f;
					activeCube.GetComponent<ClickBehavior> ().active = false;
				}
				//make new cube active
				clickedCube.transform.localScale *= 1.5f;
				clickedCube.GetComponent<ClickBehavior> ().active = true;
				activeCube = clickedCube;
			}
		} 
		else if (cubeColor == Color.white && activeCube != null) {
			//check difference of clicked white cube
			int xDiff = clickedCube.GetComponent<ClickBehavior>().myX - activeCube.GetComponent<ClickBehavior>().myX;
			int yDiff = clickedCube.GetComponent<ClickBehavior>().myY - activeCube.GetComponent<ClickBehavior>().myY;
			//if adjacent	
			if (xDiff <= 1 && yDiff <= 1) {
				//move colored cube to white cube
				clickedCube.GetComponent<Renderer>().material.color = activeCube.GetComponent<Renderer>().material.color;
				clickedCube.transform.localScale *= 1.5f;
				clickedCube.GetComponent<ClickBehavior> ().active = true;

				//make prev active cube into white cube and deactivate
				activeCube.GetComponent<Renderer>().material.color = Color.white;
				activeCube.transform.localScale /= 1.5f;
				activeCube.GetComponent<ClickBehavior> ().active = false;
				activeCube = clickedCube;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < gameTime) {
			ProcessKeyboard ();
			if (score <= 0) {
				score = 0;
			}
			if (IsRainbowPlus (x, y)) {
				score += rainbowValue;
			}
//			if (IsSameColorPlus (x, y)) {
//				score += samecolorValue;
//			}
			if (Time.time > firstTurn) {
				if (nextCubeFull) {
					score -= 1;
					grid [Random.Range (0, gridX), Random.Range (0, gridY)].GetComponent<Renderer> ().material.color = Color.black;
					nextCube.GetComponent<Renderer> ().material.color = myColors [Random.Range (0, myColors.Length)];
				} else {
					nextCube.GetComponent<Renderer> ().material.color = myColors [Random.Range (0, myColors.Length)];
					nextCubeFull = true;

				}
				firstTurn += turnLength;
			} 
			if (Time.time > gameTime) {
				nextCube.GetComponent<Renderer> ().material.color = Color.gray;
			}
		} 
		else {
			EndGame (true);
		}
	}
}