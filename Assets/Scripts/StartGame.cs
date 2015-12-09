using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	public string firstLevel;
	GameObject levelSelectionMenu;
	bool selectingLevel = false;
	
	// Use this for initialization
	void Start () {
		levelSelectionMenu = GameObject.Find ("LevelSelection");
		levelSelectionMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (selectingLevel == false) {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				Destroy (GameObject.Find ("Instructions"));
				selectingLevel = true;
				levelSelectionMenu.SetActive(true);
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				Destroy (GameObject.Find ("Canvas"));
				Application.LoadLevel (firstLevel);
			}
		}
	}
}
