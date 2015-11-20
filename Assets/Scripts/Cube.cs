using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

    // different types of cube effects
    public enum CubeEffect_e { NONE, OUTLINE, PUSH, TRAMPOLINE, SPEED, ANTI_GRAVITY };
    public CubeEffect_e effect;

    public bool active;

    // template method for applying the effect to the cube
    public void doEffect(Collider other) {
        if (!active) {
            return;
        }
        doEffectChild(other);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // actually does the effect
    public virtual void doEffectChild(Collider other) {
        // overwritten by children
    }

    public void setActive(bool active_) {
        print("Cube.setActive");
        active = active_;
        setActiveChild(active);
    }

    // OVERWRITE THIS FUNCTION IN YOUR CUBE EFFECT CLASS
    //
    // set state required for each effect
    public virtual void setActiveChild(bool active) {
        // overwritten by children
    }
}
