using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour {
    public static HideCursor S;
	void Awake() {
        Hide(true);
	}

    void Start()
    {
        S = this;
    }

    public void Hide(bool hidden)
    {
//#if (!UNITY_EDITOR)
        Cursor.visible = !hidden;
        if (hidden)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }
    }
//#endif
    }
