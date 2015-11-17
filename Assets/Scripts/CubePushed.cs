using UnityEngine;
using System.Collections;

public class CubePushed : MonoBehaviour {

    // BASIC: projectile's velocity is added to the cube's velocity
    // FIXED: the cube is moved in a constant direction and speed
    public enum PushType_e { NONE, BASIC, FIXED };
    public PushType_e pushType;

    public float basicPushDamp = 1;
    public float fixedPushDamp = 1;
    public Vector3 fixedPushDirection;


    public bool _____________;

    public Rigidbody rigidBody;

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();

        if (pushType == PushType_e.FIXED) {
            rigidBody.freezeRotation = true;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision) {
        if (collision.tag != "Projectile") {
            return;
        }
        ProjectilePush projectilePush = collision.gameObject.GetComponent<ProjectilePush>();
        if (projectilePush == null) {
            return;
        }
        push(collision.transform.position, projectilePush.getVelocity());
    }

    void push(Vector3 collisionPosition, Vector3 velocity) {
        switch (pushType) {
            case PushType_e.NONE:
                return;
            case PushType_e.BASIC:
                rigidBody.velocity += velocity * basicPushDamp;
                break;
            case PushType_e.FIXED:
                rigidBody.velocity = fixedPushDirection.normalized * velocity.magnitude * fixedPushDamp;
                break;
            default:
                break;
        }
    }
}
