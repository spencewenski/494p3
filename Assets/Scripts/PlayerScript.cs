using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private Rigidbody rigid;
    private float mouseSensitivity = 5.0f;
    private float speed = 5f;
    private float jumpSpeed = 7f;
    private Collider collider;
    private float distToGround;
    private Transform cane;
    private Transform camTrans;
    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;
        cane = this.gameObject.transform.GetChild(0);
        camTrans = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vel = rigid.velocity;
        vel.x = 0;
        vel.z = 0;
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 left = -speed * transform.right;
            vel.x += left.x;
            vel.z += left.z;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 right = speed * transform.right;
            vel.x += right.x;
            vel.z += right.z;
        }
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forward = speed * transform.forward;
            vel.x += forward.x;
            vel.z += forward.z;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 backward = -speed * transform.forward;
            vel.x += backward.x;
            vel.z += backward.z;
        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            vel.y = jumpSpeed;
        }
        rigid.velocity = vel;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        camTrans.Rotate(-mouseY, 0, 0);
        Vector3 camRot = camTrans.localRotation.eulerAngles;
        if (camRot.x > 23  && camRot.x < 200)
            camRot.x = 23;
        else if (camRot.x < 300 && camRot.x > 60)
            camRot.x = 300;
        camTrans.localRotation = Quaternion.Euler(camRot);
        
        //cane.RotateAround(transform.position, Vector3.right, -mouseY);
    }   
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0.5f), -Vector3.up, distToGround + 0.3f) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0.5f), -Vector3.up, distToGround + 0.3f) ||
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, -0.5f), -Vector3.up, distToGround + 0.3f) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, -0.5f), -Vector3.up, distToGround + 0.3f);

        //(Physics.CapsuleCast(collider..position, transform.position, .5f, -Vector3.up, out bouncehit, distToGround + 0.1f));

    }
}
