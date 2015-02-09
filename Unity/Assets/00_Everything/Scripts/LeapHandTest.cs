using UnityEngine;
using System.Collections;
using Leap;

public class LeapHandTest : MonoBehaviour {

	Controller controller;

	void Start () 
	{
		controller = new Controller();
//		controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
//		controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
//		controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
	}

	Frame lastFrame;
	
	void Update () 
	{
		Frame frame = controller.Frame();
		// do something with the tracking data in this frame

		HandList hands = frame.Hands;
		PointableList pointables = frame.Pointables;
		FingerList fingers = frame.Fingers;
		ToolList tools = frame.Tools;
		Vector translation = frame.Translation(controller.Frame(1));

//		Debug.Log (translation);

//		foreach (Gesture g in frame.Gestures())
//		{
//			Debug.Log ("gesture: " + g.Type);
//		}

//		Hand firstHand = hands[0];

//		if (firstHand.IsRight)
//		{
//			Debug.Log (firstHand.Fingers.Leftmost.TipPosition);
//			print ("position: " + firstHand.PalmPosition);
//			foreach (Finger f in fingers)
//			{
//				print("tip pos of finger " + f.Id + " is at pos: " + f.TipPosition);

//			}
//
//		}
//		lastFrame = controller.Frame();
	}
}
