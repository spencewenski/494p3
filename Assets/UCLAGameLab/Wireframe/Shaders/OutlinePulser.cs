using UnityEngine;
using System.Collections;

public class OutlinePulser : MonoBehaviour {
	
	AudioSource audio;
	Renderer renderer;
	public float volume = 4000f;
	public float minFactor;
	public float maxFactor;
	
	int qSamples = 2048;  		// array size
	float refValue = 0.1f; 		// RMS value for 0 dB
	float rmsValue;   			// sound level - RMS
	float dbValue;    			// sound level - dB
	float minInRange = 0.005f;
	
	private float[] samples; 	// audio samples
	private float[] spectrum; 	// audio samples
	
	void Start () {
		audio = GetComponent<AudioSource> ();
		renderer = GetComponent<Renderer> ();
		renderer.material.shader = Shader.Find ("UCLA Game Lab/Wireframe/Single-Sided");
		samples = new float[qSamples];
		spectrum = new float[qSamples];
	}
	
	bool isInRange() {
		audio.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris); // Window type can affect quality and speed
		for (int i = Mathf.FloorToInt(minFactor*qSamples); i < Mathf.CeilToInt(maxFactor*qSamples); ++i) {
			if (spectrum[i] > minInRange){
				return true;
			}
		}
		return false;
	}
	
	void GetVolume(){
		audio.GetOutputData(samples, 0); // fill array with samples
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
		GetVolume();
		if (isInRange ()) {
			renderer.material.SetFloat ("_Thickness", rmsValue*volume);
		} else {
			renderer.material.SetFloat ("_Thickness", Mathf.Max (renderer.material.GetFloat("_Thickness")/1.5f, 1));
		}
	}
}
