using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillPlane : MonoBehaviour {

	public float respawnDelay;
	public GameObject respawnTextPrefab;

	public bool _______________;

	public GameObject playerGO;
	public GameObject screenOutline;
	public bool flashingOutline;
	public float flashingOutlineRate;

	// Use this for initialization
	void Start () {
		screenOutline = GameObject.Find ("ChargingOutline");
		flashingOutline = false;
		flashingOutlineRate = 1.05f;
	}
	
	// Update is called once per frame
	void Update () {
		if (flashingOutline) {
			Color col = screenOutline.GetComponent<Image> ().color;
			col.a /= flashingOutlineRate;
			if (col.a < 0.001f){
				col.a = 0f;
				flashingOutline = false;
			}
			screenOutline.GetComponent<Image> ().color = col;
		}
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
        //Camera.main.transform.rotation = Checkpoint.lastCheckpoint.spawnPoint.transform.rotation;
        playerGO.transform.rotation = Checkpoint.lastCheckpoint.spawnPoint.rotation;
        playerGO.GetComponent<PlayerScript>().refreshCameraRotation();
        playerGO.GetComponent<Rigidbody>().velocity = Vector3.zero;
		// flash the outline of the screen
		flashingOutline = true;
		Color col = screenOutline.GetComponent<Image> ().color;
		col.a = 1f;
		screenOutline.GetComponent<Image> ().color = col;
	}

}
