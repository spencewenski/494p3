using UnityEngine;
using System.Collections;

public class TriggerText : MonoBehaviour {
    public float displaySec = 5f;
    public float fadeSec = 1f;

    private float displayFrames;
    private float fadeInFrames = 0;
    private float fadeOutFrames;

    private float fadeTotalFrames;

    private CanvasRenderer textRenderer;

	// Use this for initialization
	void Start () {
        displayFrames = displaySec / Time.fixedDeltaTime;
        fadeOutFrames = fadeSec / Time.fixedDeltaTime;
        fadeTotalFrames = fadeSec / Time.fixedDeltaTime;
        textRenderer = GetComponent<CanvasRenderer>();

        textRenderer.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
        ++fadeInFrames;

        if (fadeInFrames <= fadeTotalFrames)
        {
            textRenderer.SetAlpha(fadeInFrames / fadeTotalFrames);
        }

        --displayFrames;
        
        if (displayFrames <= 0)
        {
            --fadeOutFrames;
            textRenderer.SetAlpha(fadeOutFrames / fadeTotalFrames);
            
            if (fadeOutFrames <= 0)
            {
                Destroy(gameObject);
            }
        }
	}
}
