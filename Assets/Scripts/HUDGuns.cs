using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDGuns : MonoBehaviour {

    public GameObject effectTextPrefab;

    public bool __________________;

    public Dictionary<Cube.CubeEffect_e, Text> cubeEffectText = new Dictionary<Cube.CubeEffect_e, Text>();

	// Use this for initialization
	void Start () {
        foreach (Cube.CubeEffect_e effect in Shoot.S.effects) {
            addGun(effect);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Shoot.S.effects.Count > cubeEffectText.Count) {
            addGun(Shoot.S.effects[Shoot.S.effects.Count - 1]);
        }

        foreach (KeyValuePair<Cube.CubeEffect_e, Text> entry in cubeEffectText) {
            if (Shoot.S.reverseEffect()) {
                entry.Value.text = Shoot.getCubeEffectDefinition(entry.Key).reverseText;
            } else {
                entry.Value.text = Shoot.getCubeEffectDefinition(entry.Key).text;
            }
            if (entry.Key == Shoot.S.currentEffect()) {
                entry.Value.color = Shoot.getCubeEffectDefinition(entry.Key).outlineColor;
            } else {
                entry.Value.color = Color.gray;
            }
        }
    }

    private void addGun(Cube.CubeEffect_e effect) {
        EffectDefinition def = Shoot.getCubeEffectDefinition(effect);
        GameObject textGO = Instantiate(effectTextPrefab);
        textGO.transform.SetParent(transform, false);
        Text text = textGO.GetComponent<Text>();
        if (text != null) {
            text.text = def.text;
            cubeEffectText[effect] = text;
        }
    }
}
