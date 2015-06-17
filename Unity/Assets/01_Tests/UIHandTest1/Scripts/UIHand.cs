using UnityEngine;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
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
					Hand hand = hands[0].GetLeapHand(); // convert to leap hand
					// LEFT HAND
					if (hand.IsLeft)
					{
						FingerList fingers = hand.Fingers; // all fingers
						foreach (Finger finger in fingers) // go through all the fingers
						{
							if (finger.Type == Finger.FingerType.TYPE_INDEX) // get index finger
							{
								Finger index = finger;
								Bone indexB3 = index.Bone (Bone.BoneType.TYPE_DISTAL); // get the bone
								Vector3 indexB3Pos = indexB3.Center.ToUnityScaled();
								Debug.DrawRay(indexB3Pos,Vector3.up);
							}
						}
					}

					// RIGHT HAND
				}
			}
//			Debug.DrawRay(finger0Pos,Vector3.up,Color.red);
		}
	}
}