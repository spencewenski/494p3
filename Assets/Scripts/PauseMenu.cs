using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public Transform menu;
    bool paused = false;
    // Use this for initialization
    void Start() {
        menu = this.gameObject.transform;
        ToggleMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
                ToggleMenu(true);
            }
            else
            {
                Time.timeScale = 1;
                ToggleMenu(false);
            }
        }
    }
    void ToggleMenu(bool toggle)
    {
        menu.GetChild(0).gameObject.SetActive(toggle);
    }
    public void GotoMain()
    {
        Time.timeScale = 1;
        Application.LoadLevel("_SceneStart");
    }
    public void Restart()
    {
        Time.timeScale = 1;
        paused = false;
        ToggleMenu(false);
        Application.LoadLevel(Application.loadedLevel);
    }
}
