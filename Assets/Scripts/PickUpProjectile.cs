using UnityEngine;
using System.Collections;

public class PickUpProjectile : MonoBehaviour {

	public GameObject prefabProjectile;

	public bool _________________;

	public bool colliding = false;

    void Start() {
        EffectDefinition def = Shoot.getCubeEffectDefinition(prefabProjectile.GetComponent<Projectile>().effect);
        Light light = GetComponent<Light>();
        if (light != null) {
            light.color = def.outlineColor;
        }
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = def.outlineColor;
    }

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
