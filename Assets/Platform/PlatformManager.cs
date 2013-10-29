using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	
	public int numberOfObjects;
	public float recycleOffset;
	
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;

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
		//nextPosition.x += scale.x;
		int materialIndex = Random.Range(0, materials.Length);
		o.renderer.material = materials[materialIndex];
		o.collider.material = physicMaterials[materialIndex];
		objectQueue.Enqueue(o);	
		
		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
		
		if (nextPosition.y < minY) {
			nextPosition.y = minY + maxGap.y;
		}
		if (nextPosition.y > maxY) {
			nextPosition.y = maxY - maxGap.y;
		}	
	}
}
