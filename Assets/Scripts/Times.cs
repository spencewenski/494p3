using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Times : MonoBehaviour {
    public float time = 0f;
    public Text text;
    public string currTime;
    public int level;
    public string bestTime;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        float best = PlayerPrefs.GetFloat("Time" + level, 300);
        bestTime = string.Format("{0:0}:{1:00}:{2:000}", Mathf.Floor(best / 60), best % 60, best*1000%1000); ;

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        currTime = string.Format("{0:0}:{1:00}:{2:000}", Mathf.Floor(time / 60), time % 60, time*1000%1000);
        text.text = "Best: " + bestTime + "\n\t\t   " + currTime;
	}

   
}
