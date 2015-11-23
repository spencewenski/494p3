using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeController : MonoBehaviour {

    public List<Cube.CubeEffect_e> invalidEffects;

    public bool ____________________;

    public Dictionary<Cube.CubeEffect_e, Cube> cubeEffects;
    public Cube.CubeEffect_e currentEffect;
    // prevent multiple collisions from one projectile
    public bool isColliding = false;

    void Awake() {
        if (invalidEffects == null) {
            invalidEffects = new List<Cube.CubeEffect_e>();
        }
        cubeEffects = new Dictionary<Cube.CubeEffect_e, Cube>();
        // ADD EFFECT CLASSES HERE
		cubeEffects.Add(Cube.CubeEffect_e.OUTLINE, GetComponent<CubeOutline>());
        cubeEffects.Add(Cube.CubeEffect_e.PUSH, GetComponent<CubePush>());
        cubeEffects.Add(Cube.CubeEffect_e.SPEED, GetComponent<CubeSpeed>());
        cubeEffects.Add(Cube.CubeEffect_e.ANTI_GRAVITY, GetComponent<CubeAntiGravity>());
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        isColliding = false;
	}

    // detect a trigger enter event
    // 
    // updates currentEffect and applies the effect
    //
    // only care about projectiles
    void OnTriggerEnter(Collider other) {
        // prevent multiple collisions from one projectile
        if (isColliding) {
            return;
        }
        isColliding = true;
        if (other.tag != "Projectile") {
            return;
        }

        // update active effect
        Cube cubeEffect = updateCurrentEffect(other);
        // do effect
        if (cubeEffect == null) {
            return;
        }
        cubeEffect.doEffect(other);
    }

    // update the currentEffect
    //
    // returns the Cube object for the currentEffect
    private Cube updateCurrentEffect(Collider other) {
        Projectile projectileEffect = other.GetComponent<Projectile>();
        if (projectileEffect.effect == currentEffect) {
            return getCurrentEffect();
        }
        if (invalidEffects.Contains(projectileEffect.effect)) {
            return null;
        }
        // set current effect to be inactive
        Cube cubeEffect = getCurrentEffect();
        if (cubeEffect != null) {
            cubeEffect.setActive(false);
        }
        // set new effect to be active
        currentEffect = projectileEffect.effect;
        cubeEffect = getCurrentEffect();
        if (cubeEffect == null) {
            return null;
        }
        cubeEffect.setActive(true);
        //print("updateCurrentEffect");
        return cubeEffect;
    }

    // get the Cube object for the currentEffect
    private Cube getCurrentEffect() {
        if (!cubeEffects.ContainsKey(currentEffect)) {
            return null;
        }
        return cubeEffects[currentEffect];
    }
}
