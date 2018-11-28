using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    private bool flashlightEnabled;
    public GameObject flashlight;
    public GameObject lightObj;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            flashlightEnabled =! flashlightEnabled;

        if (flashlightEnabled)
            flashlight.SetActive(true);
        else
            flashlight.SetActive(false);
    }
}
