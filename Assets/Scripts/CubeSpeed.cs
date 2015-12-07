using UnityEngine;
using System.Collections;

public class CubeSpeed : Cube
{

    public override void AwakeChild()
    {
        // SET CUBE EFFECT IN YOUR AWAKE FUNCTION
        effect = Cube.CubeEffect_e.SPEED;
    }

    public override void setActiveChild(bool active_) {
        if (active_) {
            tag = "speed";
        } else {
            tag = "Untagged";
        }

    }
}
