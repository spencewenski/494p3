using UnityEngine;
using System.Collections;

public class CubeMinMaxHeight : MonoBehaviour {

	public bool useMinMaxHeight;
	public float minHeight;
	public float maxHeight;

	// Update is called once per frame
	void Update () {
		if (!useMinMaxHeight) {
			return;
		}
		Vector3 position = transform.position;
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		if (position.y >= maxHeight) {
			position.y = maxHeight;
			velocity.y = 0;
		} else if (position.y <= minHeight) {
			position.y = minHeight;
			velocity.y = 0;
		}
		transform.position = position;
		GetComponent<Rigidbody>().velocity = velocity;
	}
}
