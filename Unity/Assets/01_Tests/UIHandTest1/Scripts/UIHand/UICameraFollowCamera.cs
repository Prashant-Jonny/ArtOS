using UnityEngine;
using System.Collections;

// this class is put on UI cameras be exacty like their parents
// especially useful for Oculus VR UI cameras

public class UICameraFollowCamera : MonoBehaviour {
	

	Camera pCam; // parent camera
	Camera cam; // self camera

	int uiLayer = 5;
	int uiLayerMask;

	void Start () 
	{
		pCam = transform.parent.GetComponent<Camera> ();
		cam = GetComponent<Camera> ();
		uiLayerMask = 1 << uiLayer;
	}
	
	void Update () 
	{
		cam.fieldOfView = pCam.fieldOfView;
		cam.targetTexture = pCam.targetTexture;
		if (cam.cullingMask != uiLayerMask) 
		{
			cam.cullingMask = uiLayerMask; // only render UI layer
		}
	}
}
