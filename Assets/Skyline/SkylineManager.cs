﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {
	public Transform prefab;
	
	public int numberOfObjects;
	public float recycleOffset;
	
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	// Use this for initialization
	
	public void Start () {
		objectQueue = new Queue<Transform> (numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) {
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			//Transform o = (Transform)Instantiate(prefab);
			//o.localPosition = nextPosition;
			//nextPosition.x += o.localScale.x;
			//objectQueue.Enqueue(o);
			Recycle();
		}
	}
	
	public void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			Recycle();
		}
	}
	
	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));
		
		Vector3 position = nextPosition;
		position.x += 0.5f * scale.x;
		position.y += 0.5f * scale.y;
		
		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);	
	}
}
