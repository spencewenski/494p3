using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private Rigidbody rigid;
    private float mouseSensitivity = 5.0f;
    private float speed = 7f;
    private float jumpSpeed = 7f;
    private Collider collider;
    private float distToGround;
    private Transform cane;
    private Transform caneTip;
    private Transform camTrans;
    private float bounceX = 0f;
    private float bounceZ = 0f;
    private float bounceStrength = 20f;
    private float bounceDecreaseRate = .97f;
    private bool hasSpeed = false;
    private bool hasTramp = false;
    private LayerMask caneMask;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;
        camTrans = Camera.main.transform;
        cane = camTrans.transform.GetChild(0);
        caneTip = cane.GetChild(0);
        caneMask = ~(1 << LayerMask.NameToLayer("cane") | 1 << LayerMask.NameToLayer("kill"));
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown (KeyCode.F12)) {
			Application.LoadLevel(Application.loadedLevel);
		}
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

        float currSpeed = hasSpeed ? speed * 2 : speed;

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
        if (camRot.x > 60  && camRot.x < 200)
            camRot.x = 60;
        else if (camRot.x < 300 && camRot.x > 90)
            camRot.x = 300;
        camRot.z = 0;
        camTrans.localRotation = Quaternion.Euler(camRot);
    }   

    bool IsGrounded()
    {
        RaycastHit rayHit1 = new RaycastHit(), rayHit2 = new RaycastHit(), rayHit3 = new RaycastHit(), rayHit4 = new RaycastHit(),
            rayHit5 = new RaycastHit();

        bool isGrounded = Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0.5f), -Vector3.up, out rayHit1, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0.5f), -Vector3.up, out rayHit2, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, -0.5f), -Vector3.up, out rayHit3, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, -0.5f), -Vector3.up, out rayHit4, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(caneTip.position, -Vector3.up, out rayHit5, 1f, caneMask);
        
        if (isGrounded)
        {
            setWasLastSpeededWithRayHit(rayHit1);
            setWasLastSpeededWithRayHit(rayHit2);
            setWasLastSpeededWithRayHit(rayHit3);
            setWasLastSpeededWithRayHit(rayHit4);
            setWasLastSpeededWithRayHit(rayHit5);
        }

        //(Physics.CapsuleCast(collider..position, transform.position, .5f, -Vector3.up, out bouncehit, distToGround + 0.1f));

        return isGrounded;
    }

    void setWasLastSpeededWithRayHit(RaycastHit rayHit)
    {
        if (rayHit.collider != null)
        {
            if (rayHit.collider.gameObject.tag == "speed")
            {
                hasSpeed = true;
            } else if (rayHit.collider.gameObject.tag == "trampoline")
            {
                hasTramp = true;
            } else
            {
                hasSpeed = false;
                hasTramp = false;
            }
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "trampoline")
        {
            //foreach (ContactPoint contact in collision.contacts)
            //{
            //    Debug.DrawLine(contact.point, contact.point + contact.normal, Color.green, 2, false);
            //}
            Vector3 normal = collision.contacts[0].normal * bounceStrength;
            if (Mathf.Abs(collision.contacts[0].normal.y) != 1) {
                bounceX = normal.x;
                bounceZ = normal.z;
            }
            rigid.velocity = new Vector3(bounceX, normal.y * .55f, bounceZ);
        }
    }
    
}
