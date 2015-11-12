using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject[] prefabProjectiles;
    public float speedMult = 10f;
    //public float shootAngle = 30f;

    public bool ______________________;

    public Camera mainCamera;
    public int projectileIndex;

    void Awake() {
        mainCamera = Camera.main;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.LeftShift)) {
            shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            projectileIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            projectileIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            projectileIndex = 2;
        }
	}

    private void shoot() {
        if (projectileIndex >= prefabProjectiles.Length) {
            print("Shoot.shoot(): invalid index");
            return;
        }
        GameObject projectile = Instantiate(prefabProjectiles[projectileIndex]) as GameObject;
        Vector3 projectilePosition = transform.position;
        projectilePosition += mainCamera.transform.forward.normalized;
        projectilePosition += mainCamera.transform.up.normalized;
        //projectilePosition += mainCamera.transform.right.normalized;
        projectile.transform.position = projectilePosition;
        Vector3 velocity = Vector3.zero;
        //velocity += transform.forward;
        velocity += mainCamera.transform.forward;
        velocity = velocity.normalized * speedMult;
        projectile.GetComponent<Rigidbody>().velocity = velocity;
    }
}
