	using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	[RequireComponent (typeof(UIController))]
	public class ToolPalm : MonoBehaviour , ToolMessageTargetIF
	{
		private ToolController toolControl; // the parent ToolController

		public LeapUtil.WhichHand whichHand;
		private Hand hand;

		void Start ()
		{
			toolControl = GetComponent<ToolController>();
		}

		public void FingerButtonPressBegin(int finger)
		{
//			print ("press begin message received " + finger);

			// temp hacky dipes
			if (finger == 1)
			{
				Vector3 palmPosWorld = LeapUtil.LeapToWorldPos(hand.PalmPosition,toolControl.handController);
				Vector3 palmRotWorld = LeapUtil.LeapToWorldRot(hand.PalmNormal, toolControl.handController);
				Quaternion palmRotWorldQuat = Quaternion.LookRotation(palmRotWorld);

				GameObject.Instantiate(Resources.Load ("Cube"), palmPosWorld, palmRotWorldQuat);
			}
			if (finger == 2)
			{
				print ("select tool");
				SelectTool ();
			}
		}

		public void SelectTool()
		{
			
		}

		public void FingerButtonPressEnd(int finger)
		{
//			print ("press end message received " + finger);

		}

		void LateUpdate ()
		{
			// GET LEFT OR RIGHT HAND
			HandModel[] hands = toolControl.handController.GetAllGraphicsHands();
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

		}
	}
}