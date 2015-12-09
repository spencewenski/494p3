using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public float secToWait = 2;

    private float framesToWait;

	public string firstLevel;

	// Use this for initialization
	void Start () {
        framesToWait = secToWait / Time.fixedDeltaTime;
	}
	
	void FixedUpdate () {
        --framesToWait;
        if (Input.anyKeyDown && framesToWait < 0) {
            Destroy(GameObject.Find("Canvas"));
            Application.LoadLevel(firstLevel);

        }
	}
}
