using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlybySpawner : MonoBehaviour {
	public Transform template;
	public Vector3 direction;
	public float speed;
	public float secondsBetweenSpawn;
	public float maxDistance;

	List<Object> spawnedObjects = new List<Object>();
	float lastSpawnTime;

	// Use this for initialization
	void Start () {
		Object created = Instantiate(template,
		                                new Vector3 (transform.position.x, transform.position.y, transform.position.z),
		                                Quaternion.identity);
		spawnedObjects.Add (created);
		lastSpawnTime = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Transform toDestroy = null;
		foreach (Transform spawnedObject in spawnedObjects) {
			spawnedObject.gameObject.transform.Translate (direction * speed * Time.deltaTime);
			if (Vector3.Distance(spawnedObject.transform.position, transform.position) > maxDistance){
				toDestroy = spawnedObject;
				break;
			}
		}
		if (toDestroy != null) {
			spawnedObjects.Remove (toDestroy);
			Destroy (toDestroy.gameObject);
		}
		if (Time.time - lastSpawnTime == secondsBetweenSpawn) {
			Object created = Instantiate(template,
			                             new Vector3 (transform.position.x, transform.position.y, transform.position.z),
			                             Quaternion.identity);
			spawnedObjects.Add (created);
			lastSpawnTime = Time.time;
		}
	}
}
