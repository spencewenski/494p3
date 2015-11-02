using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightRipple : MonoBehaviour {

    public enum DistanceTweenFunction_e { TIME, FACTOR }
    public DistanceTweenFunction_e distanceTweenFunction;
    public float rippleTimeout = 1f;
    public float maxRippleDistance = 1f;
    public float tweenFactor = 0.075f;
    public float rippleSpeed = 2;
    public float rippleWidth = 1;
    public int numConcentricRipples = 2;

    private float rippleTimeoutRemaining; // time between two ripples

    const int maxRipples = 10;
    private List<Ripple> ripples = new List<Ripple>();
    private int nextRipple = 0;
	private int rippleCount = 0;

    private Material material;

    class Ripple {
        private float currentRippleDistance = 0f;
        private float rippleTime = 0f;
        private bool rippling = false;
        private DistanceTweenFunction_e distanceTweenFunction;

        public Ripple(DistanceTweenFunction_e distanceTweenFunction_) {
            distanceTweenFunction = distanceTweenFunction_;
        }

        public bool Start(Vector3 collisionPosition) {
            if (rippling) {
                return false;
            }
            rippleTime = 0f;
            currentRippleDistance = 0f;
            rippling = true;
            return true;
        }

        private bool updateFactor(float maxRippleDistance, float tweenFactor) {
            currentRippleDistance += (maxRippleDistance - currentRippleDistance) * tweenFactor;
            if ((maxRippleDistance - currentRippleDistance) < 0.001) {
				rippling = false;
                return false;
            }
            return true;
        }

        private bool updateTime(float maxRippleDistance, float rippleSpeed) {
            currentRippleDistance = rippleSpeed * rippleTime;
            if (currentRippleDistance > maxRippleDistance) {
                rippling = false;
                return false;
            }
            rippleTime += Time.deltaTime;
            return true;
        }

        public bool Update(float maxRippleDistance, float tweenFactor, float rippleSpeed) {
            if (!rippling) {
                return false;
            }
            switch (distanceTweenFunction) {
                case DistanceTweenFunction_e.FACTOR:
                    return updateFactor(maxRippleDistance, tweenFactor);
                case DistanceTweenFunction_e.TIME:
                    return updateTime(maxRippleDistance, rippleSpeed);
                default:
                    return false;
            }
        }

        public float getCurrentRippleDistance() {
            return currentRippleDistance;
        }
    }

    void Awake() {
        for (int i = 0; i < maxRipples; ++i) {
            ripples.Add(new Ripple(distanceTweenFunction));
        }
        material = GetComponent<Renderer>().material;

        material.SetFloat("_RippleWidth", rippleWidth);
        material.SetFloat("_NumConcentricRipples", numConcentricRipples);
    }

    
    // Use this for initialization
    void Start () {
        
    }

    private bool updateShaderParams() {
        for (int i = 0; i < maxRipples; ++i) {
            if (!ripples[i].Update(maxRippleDistance, tweenFactor, rippleSpeed)) {
                continue;
            }
            material.SetVector("_RippleDistance" + i, new Vector2(ripples[i].getCurrentRippleDistance(), 0f));
            //material.SetFloat("_RippleDistance" + i, ripples[i].getCurrentRippleDistance());
        }
        return true;
    }

    // Update is called once per frame
    void Update () {
        updateShaderParams();
        rippleTimeoutRemaining = Utility.updateTimeRemaining(rippleTimeoutRemaining);
    }

    private void startRipple(Vector3 collisionPosition) {
        if (!ripples[nextRipple].Start(collisionPosition)) {
            return;
        }
        material.SetVector("_ContactPosition" + nextRipple, collisionPosition);
        nextRipple = ++nextRipple % maxRipples;
		rippleCount = Mathf.Min(rippleCount + 1, maxRipples);
        material.SetInt("_RippleCount", rippleCount);
		print(rippleCount);
    }

    void OnCollisionEnter(Collision collider) {
        if (rippleTimeoutRemaining > 0f) {
            return;
        }
        rippleTimeoutRemaining = rippleTimeout;

        Vector3 collisionPosition = collider.contacts[0].point;
        startRipple(collisionPosition);
    }
}
