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
        bestTime = format(best);

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        currTime = format(time);
        text.text = bestTime + " - Best" + "\n" + currTime;
	}
    public string format(float time)
    {
        return string.Format("{0:0}:{1:00}.{2:000}", Mathf.Floor(time / 60), time % 60, time * 1000 % 1000);
    }

}
