using UnityEngine;
using System.Collections;

public class TriggerText : MonoBehaviour {
    public float displaySec = 5f;
    public float fadeSec = 1f;

    public bool _______________;

    public enum DisplayState_e { FADE_IN, VISIBLE, FADE_OUT };
    public DisplayState_e state;

    public float displayTimeRemaining;
    public float fadeTimeRemaining;

    public CanvasRenderer textRenderer;

	// Use this for initialization
	void Start () {
        textRenderer = GetComponent<CanvasRenderer>();
        fadeTimeRemaining = fadeSec;
        state = DisplayState_e.FADE_IN;
        textRenderer.SetAlpha(0);
	}
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case DisplayState_e.FADE_IN:
                fadeIn();
                return;
            case DisplayState_e.VISIBLE:
                visible();
                return;
            case DisplayState_e.FADE_OUT:
                fadeOut();
                return;
            default:
                print("TriggerText.Update(): invalid state");
                return;
        }
    }

    private void fadeIn() {
        fadeTimeRemaining = Utility.updateTimeRemaining(fadeTimeRemaining);
        if (fadeTimeRemaining <= 0) {
            displayTimeRemaining = displaySec;
            state = DisplayState_e.VISIBLE;
            textRenderer.SetAlpha(1);
            return;
        }
        textRenderer.SetAlpha((fadeSec - fadeTimeRemaining) / fadeSec);
    }

    private void visible() {
        displayTimeRemaining = Utility.updateTimeRemaining(displayTimeRemaining);
        if (displayTimeRemaining <= 0) {
            fadeTimeRemaining = fadeSec;
            state = DisplayState_e.FADE_OUT;
            return;
        }
    }

    private void fadeOut() {
        fadeTimeRemaining = Utility.updateTimeRemaining(fadeTimeRemaining);
        if (fadeTimeRemaining <= 0) {
            Destroy(gameObject);
        }
        textRenderer.SetAlpha(fadeTimeRemaining / fadeSec);
    }
}
