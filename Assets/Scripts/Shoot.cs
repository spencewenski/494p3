using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour {
    public static Shoot S;
    public static Dictionary<Cube.CubeEffect_e, EffectDefinition> EFFECT_DEFS;

    public List<EffectDefinition> cubeEffectDefinitions;

    //public List<GameObject> prefabProjectiles = new List<GameObject>();
    public GameObject prefabProjectile;
    public List<Cube.CubeEffect_e> effects = new List<Cube.CubeEffect_e>();
    public float maxChargeTime = 2f;
    public float projectileHeight = 0.5f;
	public float verSpread;
	public float horSpread;
	public int numScatterShots;

    public bool ______________________;

    public bool charging = false;
    public float chargeTime;
    public Camera mainCamera;
	public int projectileIndex;
	public GameObject chargingOutline;

    void Awake() {
        S = this;
        EFFECT_DEFS = new Dictionary<Cube.CubeEffect_e, EffectDefinition>();
        foreach (EffectDefinition def in cubeEffectDefinitions) {
            EFFECT_DEFS[def.effect] = def;
        }
        mainCamera = Camera.main;
        projectileIndex = 0;
		chargingOutline = GameObject.Find ("ChargingOutline");
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
			if (effects.Count == 0) {
				print("Update: can't shoot without gun");
				return;
			}
            startCharging();
        } else if (charging && (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.LeftControl) || chargeTime > maxChargeTime)) {
			if (chargeTime > maxChargeTime)
				scatterShoot ();
			else 
				shoot();
			stopCharging();
        }
        if (charging) {
            chargeTime += Time.deltaTime;
			Color col = chargingOutline.GetComponent<Image> ().color;
			col = Shoot.getCubeEffectDefinition(Shoot.S.currentEffect()).outlineColor;
			col.a = chargeTime / maxChargeTime;
			chargingOutline.GetComponent<Image> ().color = col;
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
		Color col = chargingOutline.GetComponent<Image> ().color;
		col.a = 0;
		chargingOutline.GetComponent<Image> ().color = col;
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
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.setEffect(currentEffect(), reverseEffect());
        projectile.setVelocity(mainCamera.transform.forward, 1f);
    }

	private void scatterShoot() {
		if (projectileIndex >= effects.Count) {
			print("scatterShoot.shoot(): invalid index");
			return;
		}

		for (int i = 0; i < numScatterShots; ++i) {
			Vector3 spreadNoise = new Vector3(Random.Range(-horSpread, horSpread),
			                                  Random.Range(-verSpread, verSpread), Random.Range(-horSpread, horSpread));
			GameObject projectileGO = Instantiate (prefabProjectile) as GameObject;
			Physics.IgnoreCollision (projectileGO.GetComponent<Collider> (), GetComponent<Collider> ());
			Vector3 projectilePosition = mainCamera.transform.position;
			projectilePosition += projectileHeight * mainCamera.transform.up.normalized;
			projectileGO.transform.position = projectilePosition;
			Projectile projectile = projectileGO.GetComponent<Projectile> ();
			projectile.setEffect (currentEffect (), reverseEffect ());
			projectile.setVelocity (mainCamera.transform.forward+spreadNoise, 1f);
		}
	}

    public Cube.CubeEffect_e currentEffect() {
        if (effects.Count == 0) {
            return Cube.CubeEffect_e.NONE;
        }
        return effects[projectileIndex];
    }

    public bool reverseEffect() {
        return Input.GetMouseButton(1);
    }
}
