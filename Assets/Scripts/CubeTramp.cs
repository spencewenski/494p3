using UnityEngine;
using System.Collections;

public class CubeTramp : Cube {

    OutlinePulser outline;

    public override void AwakeChild()
    {
        // SET CUBE EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.TRAMP;
    }

    void Start()
    {
        outline = gameObject.GetComponentInChildren<OutlinePulser>();
    }

    public override void doEffectChild(Collider collision)
    {
        outline.OutlinePulseOn = true;
        outline.outlineColor = Color.red;
        outline.accentColor = Color.magenta;
        tag = "trampoline";
    }
}
