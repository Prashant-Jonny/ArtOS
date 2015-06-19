using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
		public Transform cameraTransform; // used to figure out if palm is turned toward camera
		public Transform ui;
		public CanvasRenderer[] leftHandUI;
		public bool[] leftHandUIPress;
		public bool[] leftHandUIPressEvent;
		private CanvasRenderer squareCanvas;
		public HandController handController = null;
		public Hand handL;
		public Hand handR;

		public float buttonPressDistance; // .005f
		public float buttonPressDistancePinky; // .003f 
		public float handOffset; //.02f
		public float minimumConfidence; //.5f
		public float minimumDotFaceCamera; // 0f

		void Start ()
		{
			leftHandUIPress = new bool[leftHandUI.Length];
			leftHandUIPressEvent = new bool[leftHandUI.Length];
			// set all bool arrays to false
			for (int i = 0; i < leftHandUIPress.Length; i++)
			{
				leftHandUIPress[i] = false;
				leftHandUIPressEvent[i] = false;
			}
		}

		void Update ()
		{

		}

		// triggered
		private void OnFingerButtonPressBegin (int finger)
		{
//			Debug.Log ("press begin finger " + finger);
			Button button = leftHandUI[finger].transform.GetComponent<Button>();
			if (button != null)
				button.onClick.Invoke();
				
		}

		private void OnFingerButtonPressEnd (int finger)
		{
//			Debug.Log ("press end finger " + finger);
		}

		void LateUpdate () 
		{
			if (handController == null)
				return;
			
			HandModel[] hands = handController.GetAllGraphicsHands();
			if (hands.Length > 0)
			{
				for(int i=0; i < hands.Length; i++) // go through all hands in scene
				{
					Hand hand = hands[i].GetLeapHand(); // convert to leap hand
					if (hand.IsLeft)
					{
						handL = hand;
						// if hand is tracking well and palm is facing away from camera
						if (hand.Confidence > minimumConfidence  
						    && LeapUtil.CheckPalmFacingCamera(hand, handController, cameraTransform, minimumDotFaceCamera))
						{
							UIAlphaToggle(true); // unhide UI
							FingerList fingers = hand.Fingers;
							for (int f = 0; f < fingers.Count; f++)
							{
								Finger finger = fingers[f];

								// get position and rotation of finger ends
								Vector3 palmNormalWorld = LeapUtil.LeapToWorldRot(hand.PalmNormal, handController);
								Bone b3 = finger.Bone (Bone.BoneType.TYPE_DISTAL); // get bone 3 (end of finger)
								Vector3 b3PosWorld = LeapUtil.LeapToWorldPos(b3.Center, handController);
								Vector3 bB3RotWorld = LeapUtil.LeapToWorldRot(b3.Direction, handController);
								// ofset that position out from the palm normal
								b3PosWorld += (palmNormalWorld * handOffset);

								// set position and rotation of UI element
								leftHandUI[f].transform.position = b3PosWorld; 
								leftHandUI[f].transform.forward = palmNormalWorld;

								// calculate distance to palm
								float distanceFromPalmNormal = LeapUtil.DistanceFromPalmNormal(b3PosWorld,hand,handController);
								// check if distance is over max
								float distance = buttonPressDistance;
								if (f == 4) // if pinky
									distance = buttonPressDistancePinky; // set to pinky distance
								if (distanceFromPalmNormal > distance)
								{
									leftHandUIPress[f] = true;
								} else {
									leftHandUIPress[f] = false;
								}
							}
						}
						else 
						{
							UIAlphaToggle(false); // hide UI
						}
					} else {
						handL = null;
					}
				}
			}
			DetectFingerButtonPressEvents();
		}

		// UTILITY FUNCTIONS
		
		private void DetectFingerButtonPressEvents ()
		{
			for (int i = 0; i < leftHandUI.Length; i++)
			{
				if (leftHandUIPress[i] && leftHandUIPressEvent[i])
				{
					leftHandUIPressEvent[i] = false;
					// BUTTON PRESS BEGIN EVENT
					OnFingerButtonPressBegin(i);
				}
				
				if (!leftHandUIPress[i] && !leftHandUIPressEvent[i])
				{
					leftHandUIPressEvent[i] = true;
					// BUTTON PRESS END EVENT
					OnFingerButtonPressEnd(i);
				} 
				
			}
		}

		private void UIAlphaToggle (bool on)
		{
			for (int i = 0; i < leftHandUI.Length; i++)
			{
				if (on)
				{
					leftHandUI[i].SetAlpha(1);
					leftHandUI[i].transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(1);
				}
				if (!on)
				{
					leftHandUI[i].SetAlpha(0);
					leftHandUI[i].transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(0);
				}
			}
		}
	}
}