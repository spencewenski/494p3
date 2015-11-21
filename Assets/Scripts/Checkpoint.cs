using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public static Checkpoint lastCheckpoint;

    public Transform spawnPoint;

    public bool ______________;

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
        lastCheckpoint = this;
    }
}
