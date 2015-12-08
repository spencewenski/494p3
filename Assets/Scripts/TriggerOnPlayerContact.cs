using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TriggerOnPlayerContact : MonoBehaviour {

    public GameObject effectTextPrefab;
    public GameObject triggerTextRoot;

    public List<GameObject> outlineOnTrigger = new List<GameObject>();
    public string displayOnTrigger = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        foreach (GameObject gameObjectToOutline in outlineOnTrigger) {
            // set on outline if the object is a cube
            CubeController cubeController = gameObjectToOutline.GetComponent<CubeController>();
            if (cubeController != null && cubeController.currentEffect == Cube.CubeEffect_e.NONE)
            {
                cubeController.updateCurrentEffect(Cube.CubeEffect_e.OUTLINE);
            } else if (cubeController == null)
            {
                // if not a cube, set outlinepulser on directly
                OutlinePulser outlinePulser = gameObjectToOutline.GetComponent<OutlinePulser>();
                if (outlinePulser != null) outlinePulser.OutlinePulseOn = true;
                else
                {
                    // if no outlinepulser, try nonFadeOutlinePulser
                    NonFadeOutlinePulser nonFadeOutlinePulser = gameObjectToOutline.GetComponent<NonFadeOutlinePulser>();
                    if (nonFadeOutlinePulser != null) nonFadeOutlinePulser.OutlinePulseOn = true;
                }
            }
        }

        if (displayOnTrigger != null)
        {
            GameObject textGO = Instantiate(effectTextPrefab);
            textGO.transform.SetParent(triggerTextRoot.transform, false);
            Text text = textGO.GetComponent<Text>();
            text.text = displayOnTrigger;
            text.color = Color.white;
        }

        Destroy(gameObject);
    }
}
