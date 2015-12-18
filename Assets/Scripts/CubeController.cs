using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeController : MonoBehaviour {

	public Vector3 startPosition;
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
        cubeEffects.Add(Cube.CubeEffect_e.TRAMP, GetComponent<CubeTramp>());
    }

    // Use this for initialization
    void Start () {
		startPosition = transform.position;
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
    void OnCollisionEnter(Collision other) {
        // prevent multiple collisions from one projectile
        if (isColliding) {
            return;
        }
        isColliding = true;
        if (other.gameObject.tag != "Projectile") {
            return;
        }

        // update active effect
        Cube cubeEffect = updateCurrentEffect(other.gameObject.GetComponent<Projectile>());
        // do effect
        if (cubeEffect == null) {
            return;
        }
        cubeEffect.doEffect(other.gameObject);
    }

    private Cube updateCurrentEffect(Projectile projectile)
    {
        return updateCurrentEffect(projectile.effect);
    }

    // update the currentEffect
    //
    // returns the Cube object for the currentEffect
    public Cube updateCurrentEffect(Cube.CubeEffect_e desiredCubeEffect) {
        if (desiredCubeEffect == currentEffect) {
            return getCurrentEffect();
        }
        if (invalidEffects.Contains(desiredCubeEffect)) {
            return null;
        }
        // set current effect to be inactive
        Cube cubeEffect = getCurrentEffect();
        if (cubeEffect != null) {
            cubeEffect.setActive(false);
        }
        // set new effect to be active
        currentEffect = desiredCubeEffect;
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
