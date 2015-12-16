using UnityEngine;
using System.Collections;

public class SwitchLevel : MonoBehaviour {

	public string nextLevel;
    public float level;
    public float timeTaken;
    public Times timeScript;
    public bool bestTime;
    public bool pause = false;
    public GameObject bestText;
	public bool ___________________;
    void Start()
    {
        GameObject go = GameObject.Find("Timer");
        timeScript = go.GetComponent<Times>();
        level = timeScript.level;
        bestText = go.transform.GetChild(0).gameObject;
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
            PlayerPrefs.SetFloat("Time" + level, timeScript.time);

        }
        else if(!pause)
        {
            Application.LoadLevel(nextLevel);
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
                Application.LoadLevel(nextLevel);
            }
        }
    }

}
