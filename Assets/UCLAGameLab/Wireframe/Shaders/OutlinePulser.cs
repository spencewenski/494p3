using UnityEngine;
using System.Collections;

public class OutlinePulser : MonoBehaviour {

	AudioSource audio;
	Renderer renderer;


	int qSamples = 1024;  		// array size
	float refValue = 0.1f; 		// RMS value for 0 dB
	float rmsValue;   			// sound level - RMS
	float dbValue;    			// sound level - dB
	float volume = 5000; 		// set how much the scale will vary
	
	private float[] samples; 	// audio samples
		
	void Start () {
		audio = GetComponent<AudioSource> ();
		renderer = GetComponent<Renderer> ();
		renderer.material.shader = Shader.Find ("UCLA Game Lab/Wireframe/Single-Sided");
		samples = new float[qSamples];
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
		print (renderer.material.GetFloat("_Thickness"));
		renderer.material.SetFloat ("_Thickness", volume * rmsValue);
	}
}
