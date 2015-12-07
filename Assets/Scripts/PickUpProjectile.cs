using UnityEngine;
using System.Collections;

public class PickUpProjectile : MonoBehaviour {

    //public GameObject prefabProjectile;
    public Cube.CubeEffect_e effect;

	public bool _________________;

	public bool colliding = false;

    void Start() {
        EffectDefinition def = Shoot.getCubeEffectDefinition(effect);
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
		Shoot.S.addEffect(effect);
		Destroy(gameObject);
	}
}
