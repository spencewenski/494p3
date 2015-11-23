using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    // effect that this projectile applies
    public Cube.CubeEffect_e effect;
    public float speed = 20f;
	public Color outlineColor;
	public Color accentColor;

    public bool ___________________;

    public Rigidbody rigidBody;

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
