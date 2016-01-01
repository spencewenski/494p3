using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F12)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
