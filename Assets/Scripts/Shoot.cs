using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject prefabProjectile;
    public float speedMult = 10f;
    //public float shootAngle = 30f;

    public bool ______________________;

    public Camera mainCamera;

    void Awake() {
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Mouse0)) {
            shoot();
        }
	}

    private void shoot() {
        GameObject projectile = Instantiate(prefabProjectile) as GameObject;
        Vector3 projectilePosition = transform.position;
        projectilePosition += mainCamera.transform.forward.normalized;
        projectilePosition += mainCamera.transform.up.normalized;
        projectile.transform.position = projectilePosition;
        //Vector3 velocity = transform.forward * speedMult;
        //velocity.y = (transform.localRotation * velocity).y;
        Vector3 velocity = Vector3.zero;
        velocity += transform.forward;
        velocity += mainCamera.transform.forward;
        //velocity = Quaternion.Euler(shootAngle, 0, 0) * velocity;
        velocity = velocity.normalized * speedMult;
        projectile.GetComponent<Rigidbody>().velocity = velocity;
    }
}
