﻿using UnityEngine;
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

			if (finger == 1)
			{
				if (handOpposite != null && handControl != null)
				{
					Vector3 spawnPos = LeapUtil.LeapToWorldPos(handOpposite.PalmPosition, handControl);
					Vector3 spawnRot = LeapUtil.LeapToWorldRot(handOpposite.PalmNormal, handControl);
					Quaternion spawnRotQuat = Quaternion.LookRotation(spawnRot);

					GameObject.Instantiate(Resources.Load ("Cube"), spawnPos, spawnRotQuat);
				}
			}
			if (finger == 2)
			{
				ToolSelect ();
			}
		}


		public void ToolSelect()
		{
			// get the opposite hands UIrigidHand component
			UIHandRigidHand rigidHandOpposite;
			if (handModelOpposite != null)
				rigidHandOpposite = handModelOpposite.transform.GetComponent<UIHandRigidHand>();
			else
				return;

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
			HandModel[] hands = handControl.GetAllPhysicsHands();
			if (hands.Length > 0)
			{
				if (whichHand == LeapUtil.WhichHand.Left) 
				{
					for(int i=0; i < hands.Length; i++) // go through all hands in scene
					{
						Hand currentHand = hands[i].GetLeapHand(); // convert to leap hand
						if (currentHand != null && currentHand.IsLeft)
						{
							hand = currentHand;
							handModel = handControl.GetHandModelForLeapId(hand.Id);
						}
						if (currentHand != null && currentHand.IsRight) 
						{
							handOpposite = currentHand;
							handModelOpposite = handControl.GetHandModelForLeapId(handOpposite.Id);
						}
					}
				}

				if (whichHand == LeapUtil.WhichHand.Right) 
				{
					for(int i=0; i < hands.Length; i++) // go through all hands in scene
					{
						Hand currentHand = hands[i].GetLeapHand(); // convert to leap hand
						if (currentHand != null && currentHand.IsRight)
						{
							hand = currentHand;
							handModel = handControl.GetHandModelForLeapId(hand.Id);
						}
						if (currentHand != null && currentHand.IsLeft) 
						{
							handOpposite = currentHand;
							handModelOpposite = handControl.GetHandModelForLeapId(handOpposite.Id);
						}
					}
				}
			}
		}
	}
}