using UnityEngine;
using System.Collections;

public class CubeOutline : Cube {
	OutlinePulser outline;

	void Start() {
		outline = gameObject.GetComponentInChildren<OutlinePulser> ();
	}

	public override void doEffectChild(Collider collision) {
		outline.OutlinePulseOn = true;
		outline.outlineColor = Color.white;
		outline.accentColor = Color.red;
        tag = "Untagged";
	}
}
