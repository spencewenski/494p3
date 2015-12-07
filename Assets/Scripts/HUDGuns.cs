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
        foreach (GameObject prefabProjectile in Shoot.S.prefabProjectiles) {
            addGun(prefabProjectile);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Shoot.S.prefabProjectiles.Count > cubeEffectText.Count) {
            addGun(Shoot.S.prefabProjectiles[Shoot.S.prefabProjectiles.Count - 1]);
        }

        foreach (KeyValuePair<Cube.CubeEffect_e, Text> entry in cubeEffectText) {
            if (entry.Key == Shoot.S.currentEffect()) {
                entry.Value.color = Shoot.getCubeEffectDefinition(entry.Key).outlineColor;
            } else {
                entry.Value.color = Color.gray;
            }
        }
    }

    private void addGun(GameObject prefabProjectile) {
        Projectile projectile = prefabProjectile.GetComponent<Projectile>();
        EffectDefinition def = Shoot.getCubeEffectDefinition(projectile.effect);
        GameObject textGO = Instantiate(effectTextPrefab);
        textGO.transform.SetParent(transform, false);
        Text text = textGO.GetComponent<Text>();
        if (text != null) {
            text.text = def.text;
            cubeEffectText[projectile.effect] = text;
        }
    }
}
