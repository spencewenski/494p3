using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    // effect that this projectile applies
    public Cube.CubeEffect_e effect;
    public float speed = 50f;
	
    public bool ___________________;

    public Color outlineColor;
    public Color accentColor;
    public Rigidbody rigidBody;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        EffectDefinition def = Shoot.getCubeEffectDefinition(effect);
        outlineColor = def.outlineColor;
        accentColor = def.accentColor;
    }

    void Start() {
        Destroy(gameObject, 5f);
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
        if (other.tag == "CheckpointSystem") {
            return;
        }
        OnTriggerEnterChild(other);
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnterChild(Collider other) {
        // overridden by child
    }

}
