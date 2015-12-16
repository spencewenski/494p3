using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    // effect that this projectile applies
    public Cube.CubeEffect_e effect;
    public float speed = 50f;
    public bool reverse; // do the reverse of the effect
    public float minDestroyTime;
    public float maxDestroyTime;
	
    public bool ___________________;

    public Color outlineColor;
    public Color accentColor;
    public Rigidbody rigidBody;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start() {
        Destroy(gameObject, Random.Range(minDestroyTime, maxDestroyTime));
    }

    public void setEffect(Cube.CubeEffect_e effect_, bool reverse_) {
        effect = effect_;
        reverse = reverse_;
        EffectDefinition def = Shoot.getCubeEffectDefinition(effect);
        outlineColor = def.outlineColor;
        accentColor = def.accentColor;
        // set color
        Material mat = GetComponent<Renderer>().material;
        mat.color = def.outlineColor;
		// set outline color in shader
		Renderer rend = GetComponent<Renderer> ();
		if (rend.material.shader.name.Equals("Outlined/Silhouette Only"))
			rend.material.SetColor ("_OutlineColor", def.outlineColor);
    }

    // overwrite if you require special behavior
    public virtual void setVelocity(Vector3 direction, float speedFactor) {
        if (rigidBody == null) {
            rigidBody = GetComponent<Rigidbody>();
        }
        rigidBody.velocity = direction.normalized * speed * speedFactor;
    }

    public Vector3 getVelocity() {
        if (rigidBody == null) {
            rigidBody = GetComponent<Rigidbody>();
        }
        return rigidBody.velocity;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "CheckpointSystem" || other.tag == "Projectile") {
            return;
        }
        OnTriggerEnterChild(other);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnterChild(Collider other) {
        // overridden by child
    }

}
