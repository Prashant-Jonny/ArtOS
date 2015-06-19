using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

namespace UIHandTest1 
{
	[RequireComponent (typeof(UIHand))]
	public class UIPalm : MonoBehaviour 
	{
		private UIHand uiHand; // the parent UIHand component

		public LeapUtil.WhichHand whichHand;
		private Hand hand;
		public CanvasRenderer uiCanvas;

		public float handOffset; //.02f

		void Start ()
		{
			uiHand = GetComponent<UIHand>();
		}

		void Update ()
		{

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
				if (hand.Confidence > uiHand.minimumConfidence 
				    && LeapUtil.CheckPalmFacingCamera(hand, uiHand.handController, uiHand.cameraTransform, uiHand.minimumDotFaceCamera))
				{
					UIAlphaToggle(true); // unhide UI

					// ALIGN UI TO FINGERS
					// get position and rotation of finger ends
					Vector3 palmNormalWorld = LeapUtil.LeapToWorldRot(hand.PalmNormal, uiHand.handController);
					Vector3 palmPosWorld = LeapUtil.LeapToWorldPos(hand.PalmPosition, uiHand.handController);
					// ofset that position out from the palm normal
					palmPosWorld += (palmNormalWorld * handOffset);

					// set position and rotation of UI element
					uiCanvas.transform.position = palmPosWorld; 
					uiCanvas.transform.forward = palmNormalWorld;
				} else 
				{
					UIAlphaToggle(false); // hide UI
				}
			}
		}

		// UTILITY FUNCTIONS
		private void UIAlphaToggle (bool on)
		{
				if (on)
				{
					uiCanvas.SetAlpha(1);
					uiCanvas.transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(1);
				}
				if (!on)
				{
					uiCanvas.SetAlpha(0);
					uiCanvas.transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(0);
				}
		}
	}
}