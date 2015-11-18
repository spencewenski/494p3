using UnityEngine;
using System.Collections;

public class ProjectilePush : MonoBehaviour {

    public float speed = 20f;

    public bool ________________;

    public Rigidbody rigidBody;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision) {
        Destroy(gameObject);
    }

    public void setVelocity(Vector3 direction, float speedFactor) {
        rigidBody.velocity = direction.normalized * speed * speedFactor;
    }

    public Vector3 getVelocity() {
        return rigidBody.velocity;
    }
}
