using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GotoLevel : MonoBehaviour {

    public List<string> levels;

	// Update is called once per frame
	void Update () {
        
        if (!Input.GetKey(KeyCode.RightShift)) {
            return;
        }
	    if (Input.GetKeyDown(KeyCode.Alpha1)) {
            loadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            loadLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            loadLevel(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            loadLevel(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            loadLevel(4);
        }
    }

    public void loadLevel(int index) {
        SceneManager.LoadScene(levels[index]);
    }
}
