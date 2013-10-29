using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	public float acceleration;
	private bool touchingPlatform;
	public Vector3 jumpVelocity;

	// Use this for initialization
	/*void Start () {
	
	}*/
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(5f * Time.deltaTime, 0f, 0f);
		if (touchingPlatform && Input.GetButtonDown("Jump")) {
			rigidbody.AddForce(jumpVelocity, ForceMode.VelocityChange);
			touchingPlatform = false;
		}
		distanceTraveled = transform.localPosition.x;
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
