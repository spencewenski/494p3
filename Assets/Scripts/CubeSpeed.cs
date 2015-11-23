using UnityEngine;
using System.Collections;

public class CubeSpeed : Cube
{
    OutlinePulser outline;

    public override void AwakeChild()
    {
        // SET CUBE EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.SPEED;
    }

    void Start()
    {
        outline = gameObject.GetComponentInChildren<OutlinePulser>();
    }

    public override void doEffectChild(Collider collision)
    {
        outline.OutlinePulseOn = true;
        outline.outlineColor = new Color(255, 204, 0);
        outline.accentColor = Color.yellow;
        tag = "speed";
    }
}
