using UnityEngine;
using System.Collections;

public class CubeAntiGravity : Cube {

	public float drag;

    public bool _______________;

    public bool gravityReversed; // true => gravity is reversed; false => gravity is normal
    public Rigidbody rigidBody;
	public float originalDrag;

    public override void AwakeChild() {
        rigidBody = GetComponent<Rigidbody>();
        effect = CubeEffect_e.ANTI_GRAVITY;
		originalDrag = rigidBody.drag;
    }

    public override void setActiveChild(bool active_) {
        rigidBody.isKinematic = !active_;
		rigidBody.drag = active_ ? drag : originalDrag;
    }

    void FixedUpdate() {
		if (!active) {
			return;
		}
        if (gravityReversed) {
            rigidBody.AddForce(-Physics.gravity);
		}
    }

    public override void doEffectChild(Collider other) {
		Projectile projectile = other.GetComponent<Projectile>();
		gravityReversed = !projectile.reverse;
        rigidBody.useGravity = !gravityReversed;
		
    }

}
