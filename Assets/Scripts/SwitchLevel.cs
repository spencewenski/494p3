using UnityEngine;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string nextLevel;

	public bool ___________________;

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			return;
		}
		Application.LoadLevel(nextLevel);
	}
}
