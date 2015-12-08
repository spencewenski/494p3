using UnityEngine;
using System.Collections;

public class KillPlane : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.transform.position = Checkpoint.lastCheckpoint.spawnPoint.position;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			return;
		}
		if (other.tag == "LevelCube") {
			other.transform.position = other.gameObject.GetComponent<CubeController>().startPosition;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

    }
}
