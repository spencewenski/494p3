using UnityEngine;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string nextLevel;

	public bool ___________________;

	void OnCollisionEnter(Collision other) {
		if (!(other.gameObject.tag == "Player" || other.gameObject.tag == "cane")) {
			return;
		}
		Application.LoadLevel(nextLevel);
	}
}
