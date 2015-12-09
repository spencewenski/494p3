using UnityEngine;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string nextLevel;

	public bool ___________________;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag != "Player") {
			return;
		}
		Application.LoadLevel(nextLevel);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			return;
		}
		Application.LoadLevel(nextLevel);
	}
}
