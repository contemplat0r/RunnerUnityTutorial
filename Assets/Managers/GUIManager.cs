using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public GUIText gameOverText, instructionsText, runnerText;
	
	void Start () {
		gameOverText.enabled = false;
	}
	
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			GameEventManger.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}
	
	private void Start () {
		GameEventManger.GameStart += GameStart;
		GameEventManger.GameOver += gameOver;
		gameOverText.enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}
}
