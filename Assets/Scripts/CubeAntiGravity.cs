using UnityEngine;
using System.Collections;

public class CubeAntiGravity : Cube {

    public bool gravityReversed; // true => gravity is reversed; false => gravity is normal

    public bool _______________;

    public Rigidbody rigidBody;

    public override void AwakeChild() {
        rigidBody = GetComponent<Rigidbody>();
        effect = CubeEffect_e.ANTI_GRAVITY;
    }

    public override void setActiveChild(bool active_) {
        rigidBody.isKinematic = !active_;
    }

    void FixedUpdate() {
        if (active  && gravityReversed) {
            rigidBody.AddForce(-Physics.gravity);
        }
    }

    public override void doEffectChild(Collider other) {
		Projectile projectile = other.GetComponent<Projectile>();
		gravityReversed = !projectile.reverse;
        rigidBody.useGravity = !gravityReversed;
		
    }

}
