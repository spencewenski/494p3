using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public string firstLevel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown) {
            Destroy(GameObject.Find("Canvas"));
            Application.LoadLevel(firstLevel);

        }
	}
}
