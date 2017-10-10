using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
	This script does not deal with Unity's audio mixer system.
	The script is designed to works with a set of audio variations that, have same length and similar tune, and it will force all the audio's playback progress same. 
 */

public class AudioVariationSwitch : MonoBehaviour {

	public List<AudioSource> variations;

	public int currentIndex;

	public bool loop;

	bool playing = true;

	void Start () {
		if (variations == null || variations.Count == 0) {
			variations.AddRange(TryFetchAudioSameGO());
		}
		Switch(currentIndex, false);
	}

	void Update () {
		for (int i = 0; i < variations.Count; i++) {
			// Debug.Log("TimeSamples #" + i + ": " + variations[i].timeSamples);
			if (i != currentIndex) variations[i].timeSamples = variations[currentIndex].timeSamples;
		}
		
	}

	AudioSource[] TryFetchAudioSameGO () {
		return this.GetComponents<AudioSource>();
	}

	public void Switch (int index, bool ignoreSame = true) {
		if (index >= variations.Count) {
			if (loop) index = 0;
			else index = variations.Count - 1;
		}
		if (index < 0) {
			if (loop) index = variations.Count - 1;
			else index = 0;
		}

		if (index == currentIndex && ignoreSame) return;
		currentIndex = index;

		for (int i = 0; i < variations.Count; i++) {
			variations[i].mute = (i==currentIndex)? false: true;
		}
	}

	public void SwitchNext () {
		Switch (currentIndex + 1);
	}

	public void SwitchPrevious () {
		Switch (currentIndex - 1);
	}
}
