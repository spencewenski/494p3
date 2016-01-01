using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {

    public List<string> levels;

	// Update is called once per frame

    public void loadLevel(int index) {
		print ("loading level" + index.ToString());
        SceneManager.LoadScene(levels[index]);
    }
}
