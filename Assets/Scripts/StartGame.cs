﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	public string firstLevel;
	GameObject levelSelectionMenu;
	bool selectingLevel = false;
    Transform bestTimes;
	// Use this for initialization
	void Start () {
		levelSelectionMenu = GameObject.Find ("LevelSelection");
        bestTimes = levelSelectionMenu.transform.GetChild(3);
        float totalTime = 0f;
        for(int i = 1; i <= 5; i++)
        {
            float best = PlayerPrefs.GetFloat("Time" + i, 300);
            totalTime += best;
            bestTimes.GetChild(i - 1).gameObject.GetComponent<Text>().text = format(best);
        }
        
        levelSelectionMenu.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Total: " + format(totalTime);
        levelSelectionMenu.SetActive (false);
	}
    
    // Update is called once per frame
    void Update () {
		if (selectingLevel == false) {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				Destroy (GameObject.Find ("Instructions"));
				selectingLevel = true;
				levelSelectionMenu.SetActive(true);
                Utility.hideCursor(false);
            }
		} else {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {
				Destroy (GameObject.Find ("Canvas"));
				Application.LoadLevel (firstLevel);
			}
		}
	}

    public string format(float time)
    {
        return string.Format("{0:0}:{1:00}.{2:000}", Mathf.Floor(time / 60), time % 60, time * 1000 % 1000);
    }
}
