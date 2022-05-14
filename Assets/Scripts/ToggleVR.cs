using System.Collections;
using UnityEngine;
using UnityEngine.XR;

// switches between cardboard and noncardboard mode

public class ToggleVR : MonoBehaviour
{
    // calls switch method
    public void Start()
    {
        Switch();
    }
    // checks if in cardboard mode and switches to noncardboard mode and vice versa
    public void Switch()
    {

        if (UnityEngine.XR.XRSettings.loadedDeviceName == "cardboard")
        {
            StartCoroutine(LoadDevice("None"));
            Debug.Log("In daydream if statement");
        }
        else
        {
            StartCoroutine(LoadDevice("cardboard"));
        }
    }

    // loads the requested device at the next frame
    IEnumerator LoadDevice(string newDevice)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;
       UnityEngine.XR.XRSettings.enabled = true;
    }
}