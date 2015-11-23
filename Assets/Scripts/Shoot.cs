using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {

    public List<GameObject> prefabProjectiles;
    public float chargeFactor = 1f;
    public float maxChargeTime = 2f;

    public bool ______________________;

    public bool charging = false;
    public float chargeTime;
    public Camera mainCamera;
    public int projectileIndex;

    void Awake() {
		prefabProjectiles = new List<GameObject>();
        mainCamera = Camera.main;
        projectileIndex = 0;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) {
            startCharging();
        } else if (charging && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftControl) || chargeTime > maxChargeTime)) {
            shoot();
            stopCharging();
        }
        if (charging) {
            chargeTime += Time.deltaTime;
        }
        // switch gun with scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            projectileIndex = (projectileIndex + 1) % prefabProjectiles.Count;
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            projectileIndex--;
            if (projectileIndex < 0) {
                projectileIndex = prefabProjectiles.Count - 1;
            }
        }
        // switch gun with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            projectileIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            projectileIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            projectileIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            projectileIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            projectileIndex = 4;
        }
    }

    private void startCharging() {
        charging = true;
        chargeTime = 0f;
    }

    private void stopCharging() {
        charging = false;
        chargeTime = 0f;
    }

    private void shoot() {
        if (projectileIndex >= prefabProjectiles.Count) {
            print("Shoot.shoot(): invalid index");
            return;
        }
        GameObject projectile = Instantiate(prefabProjectiles[projectileIndex]) as GameObject;
        Vector3 projectilePosition = transform.position;
        // projectile position
        projectilePosition += mainCamera.transform.forward.normalized;
        projectilePosition += mainCamera.transform.up.normalized;
        projectile.transform.position = projectilePosition;
        // projectile velocity
        float speedFactor = 1 + Mathf.Clamp(chargeTime / maxChargeTime, 0, 1) * chargeFactor;
        projectile.GetComponent<Projectile>().setVelocity(mainCamera.transform.forward, speedFactor);
    }
}
