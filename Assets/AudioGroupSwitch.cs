using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
	This script works with Unity's Audio Mixer system.
	Before use the script, setup your Audio Mixer and expose all the Volume of the Audio Mixer Group you want to switch.
	Then, in the inspector, type in the Volume parameter exposed and you are good to go.
	(You can check and rename the parameter exposed in Audio Mixer Window.) 
 */

public class AudioGroupSwitch : MonoBehaviour {

	public List<string> exposedVolParams;

	public AudioMixer mixer;

	public bool loop;

	public int currentIndex;

	void Start () {
		Switch(currentIndex, false);
	}

	public void Switch (int index, bool ignoreSame = true) {
		if (index >= exposedVolParams.Count) {
			if (loop) index = 0;
			else index = exposedVolParams.Count - 1;
		}
		if (index < 0) {
			if (loop) index = exposedVolParams.Count - 1;
			else index = 0;
		}

		if (index == currentIndex && ignoreSame) return;
		currentIndex = index;

		for (int i = 0; i < exposedVolParams.Count; i++) {
			mixer.SetFloat(exposedVolParams[i], (i==currentIndex)? 0 : -80);
		}
	}

	public void SwitchNext () {
		Switch (currentIndex + 1);
	}

	public void SwitchPrevious () {
		Switch (currentIndex - 1);
	}

}
