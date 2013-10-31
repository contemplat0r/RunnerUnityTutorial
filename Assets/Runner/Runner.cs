using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	public float acceleration;
	private bool touchingPlatform;
	public float gameOverY;
	public Vector3 jumpVelocity, boostVelocity;
	private Vector3 startPosition;
	private static int boosts;

	// Use this for initialization
	void Start () {
		GameEventManger.GameStart += GameStart;
		GameEventManger.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	private void GameStart () {
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		GUIManager.SetDistance(distanceTraveled);
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f * Time.deltaTime, 0f, 0f);
		if (Input.GetButtonDown("Jump")) {
			if (touchingPlatform) {
				rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (boosts > 0) {
				rigidbody.AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);
		
		if (transform.localPosition.y < gameOverY) {
			GameEventManger.TriggerGameOver();
		}
	}
	
	void FixedUpdate () {
		if (touchingPlatform) {
			rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}
	
	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void OnCollisionExit () {
		touchingPlatform = false;
	}
	
	public static void AddBoost () {
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
}
