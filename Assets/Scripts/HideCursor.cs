using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour {

	void Awake() {
#if (!UNITY_EDITOR)
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
#endif
	}
}
