using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO_SoundOnButtonClick : MonoBehaviour {

	public string INFO = "This script only plays a sound when player presses space, to indicate a skip.";
	public AudioSource soundsource;

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
		{
			soundsource.Stop();
		 soundsource.Play();
		}
	}
}
