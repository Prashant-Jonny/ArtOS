using UnityEngine;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
		public Transform ui;
		public Transform square;
		public HandController handController = null;
		public Hand handL;
		public Hand handR;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
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
						FingerList fingers = hand.Fingers;
						foreach (Finger finger in fingers)
						{
							if (finger.Type == Finger.FingerType.TYPE_INDEX)
							{
								Finger index = finger;
								Bone indexB3 = index.Bone (Bone.BoneType.TYPE_DISTAL); // get the bone
								Vector3 indexB3WorldPos = LeapUtil.LeapToWorldPos(indexB3.Center, handController);
//								square.position = indexB3WorldPos;
//								Leap.Vector indexB3direction = finger.Direction;
//								Vector3 unityDirection = direction.ToUnityScaled(false)
//									Vector3 worldDirection = handController.transform.TransformPoint(unityDirection);
//								Debug.DrawRay(indexB3WorldPos,indexB3.Direction);
							}
						}
					}
				}
			}

//			Debug.DrawRay(finger0Pos,Vector3.up,Color.red);
		}
	}
}