using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public string firstLevel;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		// Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
			Destroy(GameObject.Find("Canvas"));
			Application.LoadLevel(firstLevel);
		}
	}
}
