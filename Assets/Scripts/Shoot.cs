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
	public float maxChargingEffectFactor;
    public float caneScaleFactor;
    public float chargePercentageExponent;
    

    public bool ______________________;

    public bool charging = false;
    public float chargeTime;
    public Camera mainCamera;
    public Transform caneEnd;
    public int projectileIndex;
	public GameObject chargingOutline;
    public PlayerScript player;
    private bool reverseEffect_;

    void Awake() {
        S = this;
        EFFECT_DEFS = new Dictionary<Cube.CubeEffect_e, EffectDefinition>();
        foreach (EffectDefinition def in cubeEffectDefinitions) {
            EFFECT_DEFS[def.effect] = def;
        }
        mainCamera = Camera.main;
        caneEnd = mainCamera.transform.GetChild(0).GetChild(1);
        projectileIndex = 0;
		chargingOutline = GameObject.Find ("ChargingOutline");
        player = GetComponent<PlayerScript>();
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
        if (shootKeyDown()) {
			if (effects.Count == 0) {
				print("Update: can't shoot without gun");
				return;
			}
            startCharging();
        } else if (charging && (shootKeyUp() || chargeTime > maxChargeTime)) {
			if (chargeTime > maxChargeTime)
				scatterShoot ();
			else 
				shoot();
			stopCharging();
        }
        if (charging) {
            chargeTime += Time.deltaTime;
            EffectDefinition def = getCubeEffectDefinition(currentEffect());
            Color col = chargingOutline.GetComponent<Image> ().color;
			col = def.outlineColor;
            float chargePercentage = chargeTime / maxChargeTime;
            float lerpValue = Mathf.Pow(chargePercentage, chargePercentageExponent);
            col.a = maxChargingEffectFactor * lerpValue;
			chargingOutline.GetComponent<Image>().color = col;
            player.caneColor = Color.Lerp(Color.white, def.outlineColor, lerpValue);
            player.caneScale = Mathf.Lerp(1, caneScaleFactor, lerpValue);
        }
        switchGun();
    }

    private bool shootKeyDown() {
        if (charging) {
            return false;
        }
        reverseEffect = Input.GetMouseButtonDown(1);
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }

    public bool reverseEffect {
        get {
            return reverseEffect_;
        }
        set {
            reverseEffect_ = value;
        }
    }

    private bool shootKeyUp() {
        return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1);
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
        switchCaneTipColor();
	}

    private void switchCaneTipColor() {
        EffectDefinition def = getCubeEffectDefinition(currentEffect());
        player.caneTipColor = def.outlineColor;
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
        player.caneColor = Color.white;
        player.caneScale = 1f;
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
        Vector3 projectilePosition = caneEnd.transform.position;
        // projectile position
        //projectilePosition += mainCamera.transform.forward.normalized;
        projectilePosition += projectileHeight * mainCamera.transform.up.normalized;
        projectileGO.transform.position = projectilePosition;
        // projectile velocity
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        projectile.setEffect(currentEffect(), reverseEffect);
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
			Vector3 projectilePosition = caneEnd.transform.position;
			projectilePosition += projectileHeight * mainCamera.transform.up.normalized;
			projectileGO.transform.position = projectilePosition;
			Projectile projectile = projectileGO.GetComponent<Projectile> ();
			projectile.setEffect(currentEffect(), reverseEffect);
			projectile.setVelocity(mainCamera.transform.forward+spreadNoise, 1f);
		}
	}

    public Cube.CubeEffect_e currentEffect() {
        if (effects.Count == 0) {
            return Cube.CubeEffect_e.NONE;
        }
        return effects[projectileIndex];
    }

   
}
