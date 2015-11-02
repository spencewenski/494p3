using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour {

	public static float updateTimeRemaining(float currentTimeRemaining) {
        if (currentTimeRemaining < Time.deltaTime) {
            return 0f;
        }
        return currentTimeRemaining - Time.deltaTime;
    }
}
