using UnityEngine;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
		public Transform ui;
		public Transform square;
		private CanvasRenderer squareCanvas;
		public HandController handController = null;
		public Hand handL;
		public Hand handR;

		public float handOffset; //.02f
		public float minimumConfidence; //.5f

//		public bool handLEnter;
//		public bool handLExit;

		void Start ()
		{
			squareCanvas = square.GetComponent<CanvasRenderer>();
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
							squareCanvas.SetColor(Color.white);
							squareCanvas.transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(1);

							FingerList fingers = hand.Fingers;
							foreach (Finger finger in fingers)
							{
								if (finger.Type == Finger.FingerType.TYPE_INDEX)
								{
									Finger index = finger;
									Vector3 palmNormal = LeapUtil.LeapToWorldRot(hand.PalmNormal, handController);
									Bone indexB3 = index.Bone (Bone.BoneType.TYPE_DISTAL); // get the bone
									Vector3 indexB3Pos = LeapUtil.LeapToWorldPos(indexB3.Center, handController);
									Vector3 indexB3Rot = LeapUtil.LeapToWorldRot(indexB3.Direction, handController);
									indexB3Pos += (palmNormal * handOffset);
									square.position = indexB3Pos;
									square.forward = palmNormal;
								}
							}
						}
						else 
						{
							squareCanvas.SetColor(new Color(1,1,1,0));
							squareCanvas.transform.GetChild(0).GetComponent<CanvasRenderer>().SetAlpha(0);
						}
					}
				}
			}
		}
	}
}