using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private Rigidbody rigid;
    private float mouseSensitivity = 5.0f;
    private float speed = 7f;
    private float jumpSpeed = 7f;
    private Collider playerCollider;
    private float distToGround;
    private Transform cane;
    private Transform caneTip;
    private Transform caneEnd;
    private Transform camTrans;
    private float bounceX = 0f;
    private float bounceZ = 0f;
    private float bounceStrength = 20f;
    private float bounceDecreaseRate = .97f;
    private bool hasSpeed = false;
    private bool hasTramp = false;
    private int speedCount = 0;
    private int trampCount = 0;
    private LayerMask caneMask;
    private Renderer caneRenderer;
    private Renderer tipRenderer;
    private Vector3 camRot = Vector3.zero;
    private Vector3 caneLocalScale;
    private Vector3 tipLocalScale;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        distToGround = playerCollider.bounds.extents.y;
        camTrans = Camera.main.transform;
        cane = camTrans.transform.GetChild(0);
        caneTip = cane.GetChild(0);
        caneEnd = cane.GetChild(1);
        caneMask = ~(1 << LayerMask.NameToLayer("cane") | 
            1 << LayerMask.NameToLayer("kill") | 1 << LayerMask.NameToLayer("projectile"));
        caneRenderer = cane.GetComponent<Renderer>();
        tipRenderer = caneTip.GetComponent<Renderer>();
        caneLocalScale = cane.localScale;
        tipLocalScale = caneTip.localScale;
    }
	
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

        //clamp rotation
        camRot.x -= mouseY;
        camRot.x = Mathf.Clamp(camRot.x, -80, 80);
        camRot.z = 0;
        camTrans.localEulerAngles = camRot;
        
    }

    public void refreshCameraRotation()
    {
        camRot.x = 0;
        camRot.z = 0;
    }

    public Color caneColor {
        set {
            caneRenderer.material.color = value;
        }
        get {
            return caneRenderer.material.color;
        }
    }

    public Color caneTipColor {
        set {
            tipRenderer.material.color = value;
        }
        get {
            return tipRenderer.material.color;
        }
    }

    public float caneScale {
        set {
            // don't scale the y axis
            Vector3 caneLocalScale_ = caneLocalScale;
            caneLocalScale_.x *= value;
            caneLocalScale_.z *= value;
            cane.localScale = caneLocalScale_;
            Vector3 tipLocalScale_ = tipLocalScale;
            tipLocalScale_.x *= value;
            tipLocalScale_.z *= value;
            caneTip.localScale = tipLocalScale_;
        }
    }

    bool IsGrounded()
    {
        RaycastHit rayHit1 = new RaycastHit(), rayHit2 = new RaycastHit(), rayHit3 = new RaycastHit(), rayHit4 = new RaycastHit(),
            rayHit5 = new RaycastHit(), rayHit6 = new RaycastHit();

        bool isGrounded = Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0.5f), -Vector3.up, out rayHit1, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0.5f), -Vector3.up, out rayHit2, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(0.5f, 0, -0.5f), -Vector3.up, out rayHit3, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(transform.position + new Vector3(-0.5f, 0, -0.5f), -Vector3.up, out rayHit4, distToGround + 0.1f, caneMask) ||
            Physics.Raycast(caneTip.position, -Vector3.up, out rayHit5, .5f, caneMask);
            Physics.Raycast(caneEnd.position, -Vector3.up, out rayHit6, .5f, caneMask);

        if (isGrounded)
        {
            speedCount = 0;
            trampCount = 0;

            setWasLastSpeededWithRayHit(rayHit1);
            setWasLastSpeededWithRayHit(rayHit2);
            setWasLastSpeededWithRayHit(rayHit3);
            setWasLastSpeededWithRayHit(rayHit4);
            setWasLastSpeededWithRayHit(rayHit5);
            setWasLastSpeededWithRayHit(rayHit6);

            hasTramp = trampCount > 0;
            hasSpeed = speedCount > 0 || hasSpeed && hasTramp;
        }

        //(Physics.CapsuleCast(collider..position, transform.position, .5f, -Vector3.up, out bouncehit, distToGround + 0.1f));

        return isGrounded;
    }

    void setWasLastSpeededWithRayHit(RaycastHit rayHit)
    {
        if (rayHit.collider != null)
        {
            if (rayHit.collider.gameObject.tag == "CheckpointSystem") return;
            if (rayHit.collider.gameObject.tag == "speed")
            {
                speedCount++;
            } else if (rayHit.collider.gameObject.tag == "trampoline")
            {
                trampCount++;
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
