using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptyAudioSource : MonoBehaviour {

    private AudioSource audioSource;
    private bool isDestroyed = false;
    private static Queue<EmptyAudioSource> activeAudios = new Queue<EmptyAudioSource>();

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        activeAudios.Enqueue(this);
        if (activeAudios.Count > 10)
        {
            EmptyAudioSource lastAudio;
            while ((lastAudio = activeAudios.Dequeue()).isDestroyed) { };
            lastAudio.destroyAudio();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!audioSource.isPlaying) destroyAudio();
	}

    public void destroyAudio()
    {
        Destroy(gameObject);
        isDestroyed = true;
    }

    bool isAlive()
    {
        return !isDestroyed;
    }
}
