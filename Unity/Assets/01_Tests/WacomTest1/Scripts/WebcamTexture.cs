using UnityEngine;
using System.Collections;


namespace WacomTest1 { 
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

	}
	
	void Update () 
	{
	
	}
}}