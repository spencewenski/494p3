using UnityEngine;
using System.Collections;

public class Oscillate : MonoBehaviour {

    public enum OscilateType_e { SINE, INTERPOLATION }

    public float height;
    public float period;

    public bool ______________;

    public float originalHeight;


	// Use this for initialization
	void Start () {
        originalHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        position.y = originalHeight + (height * Mathf.Sin((2 * Mathf.PI * Time.time) / period));
        transform.position = position;
	}
}
