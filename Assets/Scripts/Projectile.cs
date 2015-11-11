using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject audioSourcePrefab;
    public OutlinePulser.FrequencyRange_e frequencyRange;
    public Color accentColor;

    public bool __________________;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision) {
        if (audioSourcePrefab == null) {
            Destroy(gameObject);
            return;
        }
        OutlinePulser outlinePulser = collision.gameObject.GetComponent<OutlinePulser>();
        GameObject audioSource = Instantiate(audioSourcePrefab, transform.position, Quaternion.identity) as GameObject;
        if (outlinePulser != null) {
            outlinePulser.setAudioSource(audioSource.GetComponent<AudioSource>(), frequencyRange, accentColor);
        }
        Destroy(gameObject);

    }
}
