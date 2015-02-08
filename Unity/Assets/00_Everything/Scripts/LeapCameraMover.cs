using UnityEngine;
using System.Collections;
using Leap;

public class LeapCameraMover : MonoBehaviour {

	Controller controller;
	Vector3 velocity;
	float damp = .01f;

	void Start () 
	{
		controller = new Controller();
	}
	
	void Update () 
	{
		Frame frame = controller.Frame();
		HandList hands = frame.Hands;
//		foreach (Hand h in hands)
//		{
//			Vector3 vel = LeapUtil.ToVector3(h.PalmPosition);
//			transform.position += vel * damp;
//		}
//		Vector3 trans = LeapUtil.ToPositionVector3(frame.Translation(controller.Frame (1)));
//		Debug.Log (trans);
//		velocity += trans;
//		transform.position += velocity;
	}
}
