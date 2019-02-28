using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour {

    public AudioClip AmbientClip;

    public AudioSource AmbientSource;

	void Start () {

        AmbientSource.clip = AmbientClip;

        AmbientSource.Play();

	}
}
