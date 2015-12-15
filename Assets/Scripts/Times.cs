using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Times : MonoBehaviour {
    public float time = 0f;
    public Text text;
    private string currTime;
    public int level;
    private string highscore;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        float high = PlayerPrefs.GetFloat("highscore" + level, 0);
        highscore = string.Format("{0:0}:{1:00}", Mathf.Floor(high / 60), high % 60); ;

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        currTime = string.Format("{0:0}:{1:00}", Mathf.Floor(time / 60), time % 60);
        text.text = currTime + "\nHigh: " + highscore;
	}

   
}
