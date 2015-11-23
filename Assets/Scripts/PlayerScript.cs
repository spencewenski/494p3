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
    private float bounceX = 0f;
    private float bounceZ = 0f;
    private float bounceStrength = 20f;
    private float bounceDecreaseRate = .97f;
    private int speedBlockContacts = 0;
    private bool shouldKeepMomentum = false;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;
        cane = this.gameObject.transform.GetChild(0);
        camTrans = Camera.main.transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 vel = rigid.velocity;
        //manual friction for bouncing
        if (Mathf.Abs(bounceX) > 1)
        {
            bounceX *= bounceDecreaseRate;
        }
        else
        {
            bounceX = 0;
        }
        if (Mathf.Abs(bounceZ) > 1)
        {
            bounceZ *= bounceDecreaseRate;
        }
        else
        {
            bounceZ = 0;
        }
        vel.x = bounceX;
        vel.z = bounceZ;

        bool isGrounded = IsGrounded();

        shouldKeepMomentum = !isGrounded;

        float currSpeed = speedBlockContacts > 0 || shouldKeepMomentum ? speed * 2 : speed;

        //left
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 left = -currSpeed * transform.right;
            vel.x += left.x;
            vel.z += left.z;
        }
        //right
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 right = currSpeed * transform.right;
            vel.x += right.x;
            vel.z += right.z;
        }
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 forward = currSpeed * transform.forward;
            vel.x += forward.x;
            vel.z += forward.z;
        }
        //backward
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 backward = -currSpeed * transform.forward;
            vel.x += backward.x;
            vel.z += backward.z;
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            vel.y -= jumpSpeed;
            if (vel.y < 0)
                vel.y = 0;
            vel.y += jumpSpeed;
        }
        rigid.velocity = vel;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        camTrans.Rotate(-mouseY, 0, 0);
        Vector3 camRot = camTrans.localRotation.eulerAngles;
        if (camRot.x > 20  && camRot.x < 200)
            camRot.x = 20;
        else if (camRot.x < 300 && camRot.x > 60)
            camRot.x = 300;
        camTrans.localRotation = Quaternion.Euler(camRot);
        
    }   
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0.5f), -Vector3.up, distToGround + 0.1f) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0.5f), -Vector3.up, distToGround + 0.1f) ||
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, -0.5f), -Vector3.up, distToGround + 0.1f) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, -0.5f), -Vector3.up, distToGround + 0.1f);

        //(Physics.CapsuleCast(collider..position, transform.position, .5f, -Vector3.up, out bouncehit, distToGround + 0.1f));

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "trampoline")
        {
            //foreach (ContactPoint contact in collision.contacts)
            //{
            //    Debug.DrawLine(contact.point, contact.point + contact.normal, Color.green, 2, false);
            //}

            Vector3 normal = collision.contacts[0].normal * bounceStrength;
            if (Mathf.Abs(collision.contacts[0].normal.y) != 1)
            {
                bounceX = normal.x;
                bounceZ = normal.z;
            }
            rigid.velocity = new Vector3(bounceX, normal.y * .55f, bounceZ);
        }
        if (collision.collider.tag == "speed")
        {
            speedBlockContacts++;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "speed" && speedBlockContacts > 0)
        {
            speedBlockContacts--;
        }
    }
}
