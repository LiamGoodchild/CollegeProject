using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : MonoBehaviour {

    public AudioClip clips;
    public AudioSource audio;
    private bool audioPlaying = false;

    void Start()
    {
        audio.clip = clips;
    }
    void Update()
    {
        if (!audio.isPlaying)
        {
            audio.PlayDelayed(Random.Range(40f, 90f));
        }
    }

}
