using UnityEngine;
using System.Collections;

public class OutlinePulser : MonoBehaviour {
	
	public float volume = 1f;
	public bool OutlinePulseOn = false;
	
	// Choose default range
	public bool setToBass = false;
	public bool setToMid = false;
	public bool setToHigh = false;
	
	public enum FrequencyRange_e { BASS, MID, HIGH };
	//public FrequencyRange_e frequencyRange;
	
	// Define custom range
	public float minFrequency = 0f; // Minimum frequency to pulse to on scale from [0, 1]
	public float maxFrequency = 1f; // Maximum frequency to pulse to on scale from [0, 1]
	public float minRms = 0.001f;	// Minimum rms amplitude to pulse to
	
	public float accentThreshold = 0.15f; 		// If rms > threshold then accent color appears
	public float removeAccentThreshold = 0.05f; // If rms < threshold then accent goes away
	public Color outlineColor = Color.white;
	public Color accentColor = Color.yellow;
	
	AudioSource aud;
	Renderer rend;
	
	
	int qSamples = 2048;  		// array size
	float refValue = 0.1f; 		// RMS value for 0 dB
	float rmsValue;   			// sound level - RMS
	float dbValue;    			// sound level - dB
	
	private float[] samples; 	// audio samples
	private float[] spectrum; 	// audio samples
	
	void Start () {
		//aud = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		GameObject audioSource = GameObject.FindGameObjectWithTag("Audio");
		if (audioSource != null) {
			aud = audioSource.GetComponent<AudioSource>();
		}
		rend = GetComponent<Renderer> ();
		rend.material.shader = Shader.Find ("Outlined/Silhouette Only");
		samples = new float[qSamples];
		spectrum = new float[qSamples];
		rend.material.SetFloat("_Outline", 0);
		rend.material.SetColor ("_OutlineColor", outlineColor);
		if (setToBass) {
			minFrequency = 0f;
			maxFrequency = 0.005f;
			minRms = 0.03f;
		} else if (setToMid) {
			minFrequency = 0.005f;
			maxFrequency = 0.015f;
			minRms = 0.003f;
		} else if (setToHigh) {
			minFrequency = 0.015f;
			maxFrequency = 0.02f;
			minRms = 0.001f;
		}
	}
	
	bool isInRange() {
		aud.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris); // Window type can affect quality and speed
		for (int i = Mathf.FloorToInt(minFrequency*qSamples); i < Mathf.CeilToInt(maxFrequency*qSamples); ++i) {
			if (spectrum[i] > minRms){
				return true;
			}
		}
		return false;
	}
	
	void GetVolume(){
		aud.GetOutputData(samples, 0); // fill array with samples
		float sum = 0f;
		for (int i = 0; i < qSamples; i++){
			sum += samples[i]*samples[i]; // sum squared samples
		}
		rmsValue = Mathf.Sqrt(sum/qSamples); // rms = square root of average
		dbValue = 20*Mathf.Log10(rmsValue/refValue); // calculate dB
		if (dbValue < -160)
			dbValue = -160; // clamp it to -160dB min
	}
	
	void Update () {
		if (!OutlinePulseOn)
			return;
		if (aud == null) {
			return;
		}
		GetVolume();
		if (isInRange ()) {
			rend.material.SetFloat ("_Outline", rmsValue*volume);
			Color curColor = rend.material.GetColor("_OutlineColor");
			if (rmsValue > accentThreshold && curColor == outlineColor)
				rend.material.SetColor ("_OutlineColor", accentColor);
			if (curColor != Color.white) {
				Color saturate = curColor;
				saturate.b += 0.005f;
				rend.material.SetColor ("_OutlineColor", saturate);
			}
			if (rmsValue < removeAccentThreshold) {
				rend.material.SetColor ("_OutlineColor", outlineColor);
			}
		} else {
			rend.material.SetFloat ("_Outline", Mathf.Max (rend.material.GetFloat("_Outline")/1.5f, 0));
			rend.material.SetColor ("_OutlineColor", outlineColor);
		}
	}
	
	public void setAudioSource(AudioSource audioSource_, FrequencyRange_e frequencyRange_, Color accentColor_) {
		aud = audioSource_;
		accentColor = accentColor_;
		setFrequency(frequencyRange_);
	}
	
	private void setFrequency(FrequencyRange_e frequencyRange_) {
		setToBass = setToMid = setToHigh = false;
		switch (frequencyRange_) {
		case FrequencyRange_e.BASS:
			setToBass = true;
			break;
		case FrequencyRange_e.MID:
			setToMid = true;
			break;
		case FrequencyRange_e.HIGH:
			setToHigh = true;
			break;
		default:
			setToBass = setToMid = setToHigh = false;
			break;
		}
	}
}
