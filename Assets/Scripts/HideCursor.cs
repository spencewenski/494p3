﻿using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour {

	void Awake() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
