using UnityEngine;
using System.Collections;

public class PickUpProjectile : MonoBehaviour {

	public GameObject prefabProjectile;

	public bool _________________;

	public bool colliding = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag != "Player") {
			return;
		}
		if (colliding) {
			return;
		}
		colliding = true;
		Shoot shoot = other.GetComponent<Shoot>();
		shoot.prefabProjectiles.Add(prefabProjectile);
		Destroy(gameObject);
	}
}
