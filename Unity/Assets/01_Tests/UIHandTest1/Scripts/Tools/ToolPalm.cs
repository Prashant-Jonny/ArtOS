using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	[RequireComponent (typeof(ToolController))]
	public class ToolPalm : MonoBehaviour , ToolMessageTargetIF
	{
		private ToolController toolControl; // the parent ToolController
		private HandController handControl;

		public LeapUtil.WhichHand whichHand;
		private Hand hand;
		private Hand handOpposite;
		public HandModel handModel;
		public HandModel handModelOpposite;

		void Start ()
		{
			toolControl = GetComponent<ToolController>();
			handControl = toolControl.handController;
		}

		public void FingerButtonPressBegin(int finger)
		{
//			print ("press begin message received " + finger);

//			if (finger == 1)
//			{
//				Vector3 spawnPos = LeapUtil.LeapToWorldPos(hand.PalmPosition,toolControl.handController);
//				Vector3 spawnRot = LeapUtil.LeapToWorldRot(hand.PalmNormal, toolControl.handController);
//				Quaternion spawnRotQuat = Quaternion.LookRotation(spawnRot);
//
//				GameObject.Instantiate(Resources.Load ("Cube"), spawnPos, spawnRotQuat);
//			}
//			if (finger == 2)
//			{
//				ToolSelect ();
//			}
		}

		public void ToolSelect()
		{
			// get the rigidHand component
			UIHandRigidHand rigidHandOpposite = handModelOpposite.transform.GetComponent<UIHandRigidHand>();

			// get what the rigidhand is touching
			GameObject selectTarget = rigidHandOpposite.currentCollider;		
			ExecuteEvents.Execute<ArtObjectMessageTargetIF>( selectTarget, null, (x,y)=>x.Select() );

		}

		public void FingerButtonPressEnd(int finger)
		{
//			print ("press end message received " + finger);

		}

		void Update ()
		{

		}

		void LateUpdate ()
		{
			// GET LEFT OR RIGHT HAND
			HandModel[] hands = toolControl.handController.GetAllGraphicsHands();
			if (hands.Length > 0)
			{
				if (whichHand == LeapUtil.WhichHand.Left) 
				{
					for(int i=0; i < hands.Length; i++) // go through all hands in scene
					{
						Hand currentHand = hands[i].GetLeapHand(); // convert to leap hand
						if (currentHand.IsLeft)
						{
							hand = currentHand;
							handModel = toolControl.handController.GetHandModelForLeapId(hand.Id);
						}
						if (currentHand.IsRight) 
						{
							handOpposite = currentHand;
							handModelOpposite = toolControl.handController.GetHandModelForLeapId(hand.Id);
						}
					}
				}

			}

		}
	}
}