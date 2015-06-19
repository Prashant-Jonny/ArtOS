using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

namespace UIHandTest1 
{
	[RequireComponent (typeof(UIHand))]
	public class UIFingers : MonoBehaviour 
	{
		private UIHand uiHand; // the parent UIHand component

		public LeapUtil.WhichHand whichHand;
		public bool useThumb;
		public CanvasRenderer[] uiCanvases;
		private bool[] Press;
		private bool[] PressEvent;
		private Hand hand;

		public float buttonPressDistance; // .005f
		public float buttonPressDistancePinky; // .003f 
		public float handOffset; //.02f
		public float minimumConfidence; //.5f
		public float minimumDotFaceCamera; // 0f

		void Start ()
		{
			uiHand = GetComponent<UIHand>();
			Press = new bool[uiCanvases.Length];
			PressEvent = new bool[uiCanvases.Length];
			// set all bool arrays to false
			for (int i = 0; i < Press.Length; i++)
			{
				Press[i] = false;
				PressEvent[i] = false;
			}
		}

		void Update ()
		{

		}

		// triggered
		private void OnFingerButtonPressBegin (int finger)
		{
//			Debug.Log ("press begin finger " + finger);
			Button button = uiCanvases[finger].transform.GetComponent<Button>();
			if (button != null)
				button.onClick.Invoke();
				
		}

		private void OnFingerButtonPressEnd (int finger)
		{
//			Debug.Log ("press end finger " + finger);
		}

		void LateUpdate () 
		{
			// GET LEFT OR RIGHT HAND
			HandModel[] hands = uiHand.handController.GetAllGraphicsHands();
			if (hands.Length > 0)
			{
				for(int i=0; i < hands.Length; i++) // go through all hands in scene
				{
					Hand currentHand = hands[i].GetLeapHand(); // convert to leap hand
					if (whichHand == LeapUtil.WhichHand.Left && currentHand.IsLeft) 
						hand = currentHand;
					if (whichHand == LeapUtil.WhichHand.Right && currentHand.IsRight)
						hand = currentHand;
				}
			}

			// ALIGN UI TO FINGERS AND CHECK IF PRESSING
			if (hand != null)
			{
				// if hand is tracking well and palm is facing away from camera
				if (hand.Confidence > minimumConfidence 
//				    && hand.IsValid
				    && LeapUtil.CheckPalmFacingCamera(hand, uiHand.handController, uiHand.cameraTransform, minimumDotFaceCamera))
				{
					UIAlphaToggle(true); // unhide UI
					FingerList fingers = hand.Fingers;
					for (int f = 0; f < fingers.Count; f++)
					{
						Finger finger = fingers[f];

						// if not thumb
						if (!useThumb && finger.Type != Finger.FingerType.TYPE_THUMB) 
						{
					
							// ALIGN UI TO FINGERS
							// get position and rotation of finger ends
							Vector3 palmNormalWorld = LeapUtil.LeapToWorldRot(hand.PalmNormal, uiHand.handController);
							Bone b3 = finger.Bone (Bone.BoneType.TYPE_DISTAL); // get bone 3 (end of finger)
							Vector3 b3PosWorld = LeapUtil.LeapToWorldPos(b3.Center, uiHand.handController);
							Vector3 bB3RotWorld = LeapUtil.LeapToWorldRot(b3.Direction, uiHand.handController);
							// ofset that position out from the palm normal
							b3PosWorld += (palmNormalWorld * handOffset);

							// set position and rotation of UI element
							uiCanvases[f].transform.position = b3PosWorld; 
							uiCanvases[f].transform.forward = palmNormalWorld;


							// CHECK IF PRESSING
							// calculate distance to palm
							float distanceFromPalmNormal = LeapUtil.DistanceFromPalmNormal(b3PosWorld,hand,uiHand.handController);
							// check if distance is over max
							float distance = buttonPressDistance;
							if (f == 4) // if pinky
								distance = buttonPressDistancePinky; // set to pinky distance
							if (distanceFromPalmNormal > distance)
							{
								Press[f] = true;
							} else {
								Press[f] = false;
							}
						}
					}
				}
				else 
				{
					UIAlphaToggle(false); // hide UI
				}
			}
			DetectFingerButtonPressEvents();
		}

		// UTILITY FUNCTIONS
		
		private void DetectFingerButtonPressEvents ()
		{
			for (int i = 0; i < uiCanvases.Length; i++)
			{
				if (Press[i] && PressEvent[i])
				{
					PressEvent[i] = false;
					// BUTTON PRESS BEGIN EVENT
					OnFingerButtonPressBegin(i);
				}
				
				if (!Press[i] && !PressEvent[i])
				{
					PressEvent[i] = true;
					// BUTTON PRESS END EVENT
					OnFingerButtonPressEnd(i);
				} 
				
			}
		}

		private void UIAlphaToggle (bool on)
		{
			for (int i = 0; i < uiCanvases.Length; i++)
			{
				if (on)
				{
					uiCanvases[i].SetAlpha(1);
					uiCanvases[i].transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(1);
				}
				if (!on)
				{
					uiCanvases[i].SetAlpha(0);
					uiCanvases[i].transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(0);
				}
			}
		}
	}
}