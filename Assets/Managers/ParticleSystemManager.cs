using UnityEngine;
using System.Collections;

public class ParticleSystemManager : MonoBehaviour {
	public ParticleSystem[] particleSystems;

	// Use this for initialization
	void Start () {
		GameEventManger.GameStart += GameStart;
		GameEventManger.GameOver += GameOver;
		GameOver();
	
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
	
	void GameStart () {
		for (int i = 0; i < particleSystems.Length; i++) {
			particleSystems[i].Clear();
			particleSystems[i].enableEmission = true;
		}
	}
	
	void GameOver () {
		for (int i = 0; i < particleSystems.Length; i++) {
			particleSystems[i].Clear();
			particleSystems[i].enableEmission = false;
		}
	}
}
