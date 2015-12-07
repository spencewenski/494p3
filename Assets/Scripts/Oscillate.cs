using UnityEngine;
using System.Collections;

public class Oscillate : MonoBehaviour {

    public enum OscilateType_e { SINE, INTERPOLATION }

    public float height;
    public float frequency;

    public bool ______________;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        position.y += Mathf.Sin(2 * Mathf.PI * frequency * Time.time) * (height * frequency * Time.deltaTime);
        transform.position = position;
	}
}
