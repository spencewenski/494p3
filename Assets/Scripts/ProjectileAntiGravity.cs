using UnityEngine;
using System.Collections;

public class ProjectileAntiGravity : Projectile {

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        effect = Cube.CubeEffect_e.ANTI_GRAVITY;
		outlineColor = Color.yellow;
		accentColor = (Color.red + Color.yellow) / 2;

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
