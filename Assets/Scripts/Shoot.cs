using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {
    public static Shoot S;
    public static Dictionary<Cube.CubeEffect_e, EffectDefinition> EFFECT_DEFS;

    public List<EffectDefinition> cubeEffectDefinitions;

    //public List<GameObject> prefabProjectiles = new List<GameObject>();
    public GameObject prefabProjectile;
    public List<Cube.CubeEffect_e> effects = new List<Cube.CubeEffect_e>();
    public float chargeFactor = 1f;
    public float maxChargeTime = 2f;
    public float projectileHeight = 0.5f;

    public bool ______________________;

    public bool charging = false;
    public float chargeTime;
    public Camera mainCamera;
	public int projectileIndex;

    void Awake() {
        S = this;
        EFFECT_DEFS = new Dictionary<Cube.CubeEffect_e, EffectDefinition>();
        foreach (EffectDefinition def in cubeEffectDefinitions) {
            EFFECT_DEFS[def.effect] = def;
        }
        mainCamera = Camera.main;
        projectileIndex = 0;
    }

    public static EffectDefinition getCubeEffectDefinition(Cube.CubeEffect_e effect) {
        if (EFFECT_DEFS.ContainsKey(effect)) {
            return EFFECT_DEFS[effect];
        }
        return new EffectDefinition();
    }

    public void addEffect(Cube.CubeEffect_e effect) {
        if (effects.Contains(effect)) {
            return;
        }
        effects.Add(effect);
    }

    // Use this for initialization
    void Start() {
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
        switchGun();
    }

	private void switchGun() {
		// switch gun with scroll wheel
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			switchToPreviousGun();
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			switchToNextGun();
		}
		if (Input.GetKeyDown(KeyCode.Tab)) {
			switchToNextGun();
		}
		// switch gun with number keys
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			projectileIndex = validProjectileIndex(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			projectileIndex = validProjectileIndex(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			projectileIndex = validProjectileIndex(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4)) {
			projectileIndex = validProjectileIndex(3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5)) {
			projectileIndex = validProjectileIndex(4);
		}
	}

    private void switchToNextGun() {
        projectileIndex = (projectileIndex + 1) % effects.Count;
		projectileIndex = validProjectileIndex(projectileIndex);
    }

    private void switchToPreviousGun() {
        projectileIndex--;
        if (projectileIndex < 0) {
            projectileIndex = effects.Count - 1;
        }
		projectileIndex = validProjectileIndex(projectileIndex);
    }

    private int validProjectileIndex(int index) {
        if (effects.Count == 0) {
            return 0;
        }
        if (index >= effects.Count || index < 0) {
            return projectileIndex;
        }
        return index;
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
        if (projectileIndex >= effects.Count) {
            print("Shoot.shoot(): invalid index");
            return;
        }
        //GameObject projectileGO = Instantiate(prefabProjectiles[projectileIndex]) as GameObject;
        GameObject projectileGO = Instantiate(prefabProjectile) as GameObject;
        Physics.IgnoreCollision(projectileGO.GetComponent<Collider>(), GetComponent<Collider>());
        //Vector3 projectilePosition = transform.position;
        Vector3 projectilePosition = mainCamera.transform.position;
        // projectile position
        //projectilePosition += mainCamera.transform.forward.normalized;
        projectilePosition += projectileHeight * mainCamera.transform.up.normalized;
        projectileGO.transform.position = projectilePosition;
        // projectile velocity
        float speedFactor = 1 + Mathf.Clamp(chargeTime / maxChargeTime, 0, 1) * chargeFactor;
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.setEffect(currentEffect());
        projectile.setVelocity(mainCamera.transform.forward, speedFactor);
    }

    public Cube.CubeEffect_e currentEffect() {
        if (effects.Count == 0) {
            return Cube.CubeEffect_e.NONE;
        }
        return effects[projectileIndex];
    }
}
