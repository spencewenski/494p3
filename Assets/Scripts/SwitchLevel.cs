using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour {

	public string nextLevel;
    public int level;
    public float timeTaken;
    public Times timeScript;
    public bool bestTime;
    public bool pause = false;
    public GameObject bestText;
	public bool ___________________;

    void Start()
    {
        GameObject go = GameObject.Find("TimerHUD");
        timeScript = go.transform.FindChild("Timer").GetComponent<Times>();
        level = timeScript.level;
        bestText = go.transform.FindChild("BestTimePopup").gameObject;
    }

	void OnCollisionEnter(Collision other) {
		if (!(other.gameObject.tag == "Player" || other.gameObject.tag == "cane")) {
			return;
		}
        float best = PlayerPrefs.GetFloat("Time" + level, 300);
        timeTaken = timeScript.time;
        if (timeTaken < best)
        {
            bestText.SetActive(true);
            pause = true;
            PlayerPrefs.SetFloat("Time" + level, timeTaken);

        }
        else if(!pause)
        {
            SceneManager.LoadScene(nextLevel);
        }
		
	}
    void Update()
    {
        if (pause)
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

}
