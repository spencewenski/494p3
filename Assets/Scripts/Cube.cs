using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    // different types of cube effects
    public enum CubeEffect_e { NONE, OUTLINE, PUSH, TRAMP, SPEED, ANTI_GRAVITY };
    public CubeEffect_e effect;

	public bool ________________;

	public OutlinePulser outlinePulser;
	public LightRipple lightRipple;
    public bool active;

	void Awake() {
		outlinePulser = GetComponentInChildren<OutlinePulser>();
		lightRipple = GetComponent<LightRipple>();
		AwakeChild();
	}

	public virtual void AwakeChild() {
		//
	}

    // template method for applying the effect to the cube
    public void doEffect(Collider other) {
        if (!active) {
            return;
        }
		Projectile projectile = other.GetComponent<Projectile>();
		outlinePulser.outlineColor = projectile.outlineColor;
		outlinePulser.accentColor = projectile.accentColor;
		lightRipple.setRippleColor(projectile.outlineColor);
        doEffectChild(other);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // actually does the effect
    public virtual void doEffectChild(Collider other) {
        // overwritten by children
    }

    public void setActive(bool active_) {
        active = active_;
        setActiveChild(active);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // set state required for each effect
    public virtual void setActiveChild(bool active_) {
        // overwritten by children
    }
}
