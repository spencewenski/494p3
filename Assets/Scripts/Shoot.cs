﻿using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject[] prefabProjectiles;
    //public float speed = 10f;
    public float chargeFactor = 1f;
    public float maxChargeTime = 2f;

    public bool ______________________;

    public bool charging = false;
    public float chargeTime;
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
	    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift)) {
            startCharging();
        } else if (charging && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftShift) || chargeTime > maxChargeTime)) {
            shoot();
            stopCharging();
        }
        if (charging) {
            chargeTime += Time.deltaTime;
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

    private void startCharging() {
        charging = true;
        chargeTime = 0f;
    }

    private void stopCharging() {
        charging = false;
        chargeTime = 0f;
    }

    private void shoot() {
        if (projectileIndex >= prefabProjectiles.Length) {
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
        projectile.GetComponent<ProjectilePush>().setVelocity(mainCamera.transform.forward, speedFactor);
    }
}
