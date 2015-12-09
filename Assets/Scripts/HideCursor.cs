using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour
{
    public static HideCursor S;
    void Awake()
    {
    Utility.hideCursor(true);
        S = this;
    }
}
