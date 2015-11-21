using UnityEngine;
using System.Collections;

public class ProjectilePush : Projectile {

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        // SET EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.PUSH;
    }

}
