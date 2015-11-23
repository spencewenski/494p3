using UnityEngine;
using System.Collections;

public class ProjectileSpeed : Projectile
{

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        // SET EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.SPEED;
		outlineColor = Color.blue;
		accentColor = Color.cyan;
    }

}
