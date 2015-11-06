using UnityEngine;
using System.Collections;

public class OutlinePulser : MonoBehaviour {
	
	AudioSource aud;
	Renderer rend;
	public float volume = 4000f;
	public float minFactor;
	public float maxFactor;
	public float minThreshold = 0.001f;
	public float colorThreshold = 0.15f;
	public float whiteThreshold = 0.05f;
	public Color loudColor = Color.yellow;

	int qSamples = 2048;  		// array size
	float refValue = 0.1f; 		// RMS value for 0 dB
	float rmsValue;   			// sound level - RMS
	float dbValue;    			// sound level - dB

	
	private float[] samples; 	// audio samples
	private float[] spectrum; 	// audio samples
	
	void Start () {
		aud = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
		rend = GetComponent<Renderer> ();
		rend.material.shader = Shader.Find ("Outlined/Silhouette Only");
		//rend.material.shader = Shader.Find ("UCLA Game Lab/Wireframe/Single-Sided");
		samples = new float[qSamples];
		spectrum = new float[qSamples];
		// rend.material.SetFloat ("_Thickness", 0);
		rend.material.SetFloat("_Outline", 0);
		rend.material.SetColor ("_OutlineColor", Color.white);
	}
	
	bool isInRange() {
		aud.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris); // Window type can affect quality and speed
		for (int i = Mathf.FloorToInt(minFactor*qSamples); i < Mathf.CeilToInt(maxFactor*qSamples); ++i) {
			if (spectrum[i] > minThreshold){
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
		GetVolume();
		if (isInRange ()) {
			rend.material.SetFloat ("_Outline", rmsValue*volume);
			Color curColor = rend.material.GetColor("_OutlineColor");
			if (rmsValue > colorThreshold && curColor == Color.white)
					rend.material.SetColor ("_OutlineColor", loudColor);
			if (curColor != Color.white) {
				Color saturate = curColor;
				saturate.b += 0.005f;
				rend.material.SetColor ("_OutlineColor", saturate);
			}
			if (rmsValue < whiteThreshold)
					rend.material.SetColor ("_OutlineColor", Color.white);
				
			//rend.material.SetFloat ("_Thickness", rmsValue*volume);
		} else {
			rend.material.SetFloat ("_Outline", Mathf.Max (rend.material.GetFloat("_Outline")/1.5f, 0));
			rend.material.SetColor ("_OutlineColor", Color.white);
			//rend.material.SetFloat ("_Thickness", Mathf.Max (rend.material.GetFloat("_Thickness")/1.5f, 0));
		}
	}
}
