using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Audio : MonoBehaviour {

    public bool ________________;

    public AudioSource audioSource;
    
    public void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        //audioSource.mute = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
