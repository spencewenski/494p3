using UnityEngine;
using System.Collections;

public class CubeTramp : Cube {


    public override void AwakeChild()
    {
        // SET CUBE EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.TRAMP;
    }

    public override void setActiveChild(bool active_) {
        if (active_) {
            tag = "trampoline";
        } else {
            tag = "Untagged";
        }

    }
}
