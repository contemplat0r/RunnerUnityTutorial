using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	public float acceleration;
	private bool touchingPlatform;
	public float gameOverY;
	public Vector3 jumpVelocity;
	private Vector3 startPosition;

	// Use this for initialization
	void Start () {
		GameEventManger.GameStart += GameStart;
		GameEventManger.GameOver += gameOverY;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	private void GameStart () {
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
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
		if (touchingPlatform && Input.GetButtonDown("Jump")) {
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		distanceTraveled = transform.localPosition.x;
		
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
}
