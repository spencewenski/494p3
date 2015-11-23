using UnityEngine;
using System.Collections;

public class SceneReloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F12))
		    Application.LoadLevel(Application.loadedLevel);
	}
}
