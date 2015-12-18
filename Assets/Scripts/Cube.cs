using UnityEngine;
using System.Collections;

[System.Serializable]
public class EffectDefinition {
    public Cube.CubeEffect_e effect;
    public string text;
    public string reverseText;
    public Color outlineColor;
    public Color accentColor;
}

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
    public void doEffect(GameObject other) {
        if (!active) {
            return;
        }
        doEffectChild(other);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // actually does the effect
    public virtual void doEffectChild(GameObject other) {
        // overwritten by children
    }

    public void setActive(bool active_) {
        active = active_;
        // set color and turn on outline
        if (active) {
            EffectDefinition def = Shoot.getCubeEffectDefinition(effect);
            outlinePulser.OutlinePulseOn = true;
            outlinePulser.setOutlineAccentColor(def.outlineColor, def.accentColor);
            lightRipple.setRippleColor(def.outlineColor);
        }
        setActiveChild(active);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // set state required for each effect
    public virtual void setActiveChild(bool active_) {
        // overwritten by children
    }
}
