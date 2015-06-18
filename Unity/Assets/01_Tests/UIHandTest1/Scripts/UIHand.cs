using UnityEngine;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
		public Transform ui;
		public CanvasRenderer[] leftHandUI;
		private CanvasRenderer squareCanvas;
		public HandController handController = null;
		public Hand handL;
		public Hand handR;

		public float curlTolerance; // .6f
		public float handOffset; //.02f
		public float minimumConfidence; //.5f

//		public bool handLEnter;
//		public bool handLExit;

		void Start ()
		{
//			for (int i = 0; i <= leftHandUI.Length; i++)
//			{
//				
//			}
//			squareCanvas = square0.GetComponent<CanvasRenderer>();
		}

		void Update ()
		{

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
						if (hand.Confidence > minimumConfidence)
						{
							UIAlphaToggle(true);
							FingerList fingers = hand.Fingers;
							for (int f = 0; f < fingers.Count; f++)
							{
								Finger finger = fingers[f];

								// get position and rotation of finger ends
								Vector3 palmNormal = LeapUtil.LeapToWorldRot(hand.PalmNormal, handController);
								Bone b3 = finger.Bone (Bone.BoneType.TYPE_DISTAL); // get bone 3 (end of finger)
								Vector3 b3Pos = LeapUtil.LeapToWorldPos(b3.Center, handController);
								Vector3 bB3Rot = LeapUtil.LeapToWorldRot(b3.Direction, handController);
								// ofset that position out from the palm normal
								b3Pos += (palmNormal * handOffset);

								// set position and rotation of UI element
								leftHandUI[f].transform.position = b3Pos; 
								leftHandUI[f].transform.forward = palmNormal;

								// calculate distance from palm normal
								LeapUtil.DistanceFromPalmNormal(leftHandUI[f].transform.position,hand,handController);

//								if(finger.IsExtended == false)
//									Debug.Log ("pressing " + finger.Type);

								// set the color of the UI element based on curl
								// not working now because my curl function is f-ed
//								float curl = LeapUtil.FingerCurl(finger); // get curl of finger
//								if(f == 1)
//									Debug.Log (curl);
//								curl = LeapUtil.Remap(curl, -10f, -40f, 0f, 1f); // remap the value 0-1
//								leftHandUI[f].SetColor(new Color(0,curl,0,1));
							}
						}
						else 
						{
							UIAlphaToggle(false);
						}
					}
				}
			}
		}

		void UIAlphaToggle (bool on)
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