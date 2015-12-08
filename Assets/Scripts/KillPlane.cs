using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillPlane : MonoBehaviour {

	public float respawnDelay;
	public GameObject respawnTextPrefab;

	public bool _______________;

	public GameObject playerGO;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			playerGO = other.gameObject;
			Invoke ("playerRespawn", respawnDelay);
			return;
		}
		if (other.tag == "LevelCube") {
			other.transform.position = other.gameObject.GetComponent<CubeController>().startPosition;
			other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

    }

	private void playerRespawn() {
		playerGO.transform.position = Checkpoint.lastCheckpoint.spawnPoint.position;
		playerGO.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

}
