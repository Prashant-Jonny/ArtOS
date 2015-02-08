using UnityEngine;
using System.Collections;

public class WebcamTexture : MonoBehaviour {

	void Start () 
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		for( var i = 0 ; i < devices.Length; i++ )
		{
			Debug.Log(devices[i].name);
		}
		WebCamTexture webcam = new WebCamTexture();
		webcam.deviceName = "FaceTime HD Camera (Built-in)";
		gameObject.GetComponent<Renderer>().material.mainTexture = webcam;
		webcam.Play();

//		WebCamTexture webcamTexture = new WebCamTexture();
//		this.GetComponent<Renderer>().material.mainTexture = webcamTexture;
//		webcamTexture.Play();
	}
	
	void Update () 
	{
	
	}
}
