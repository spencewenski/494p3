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
        if (other.tag != "Player") {
            return;
        }
        other.transform.position = Checkpoint.lastCheckpoint.spawnPoint.position;
    }
}
