// DebugInput.cs
using UnityEngine;

public class DebugInput : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
            Debug.Log("Key detected: " + Input.inputString);

        Debug.Log("Mouse X: " + Input.GetAxis("Mouse X"));
    }
}