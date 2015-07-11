using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]

public class SfxVolumeOptionsScaler : MonoBehaviour {

	

	AudioSource aud;
	float originalVol;

	// Use this for initialization
	void Start () {
	
		aud = GetComponent<AudioSource>();
		originalVol = aud.volume;
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		aud.volume = originalVol * Options.sfxVolumeFactor * Options.sfxMuteFactor;
	
	}
}
