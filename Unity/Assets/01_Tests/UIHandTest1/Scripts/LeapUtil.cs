using UnityEngine;
using System.Collections;
using Leap;

//namespace Leap { // need the leap namespace for leap specific callbacks
namespace UIHandTest1 
{
	public class LeapUtil
	{

		public enum WhichHand {Left, Right}
	
		public static Vector3 LeapToWorldPos(Leap.Vector position, HandController handController)
		{
			Vector3 unityPosition = position.ToUnityScaled(false);
			return handController.transform.TransformPoint(unityPosition);
		}

		public static Vector3 LeapToWorldRot(Leap.Vector direction, HandController handController)
		{
			Vector3 unityDirection = direction.ToUnity(false);
			return handController.transform.TransformDirection(unityDirection);
		}

		public static float DistanceFromPalmNormal(Vector3 position, Hand hand, HandController handController)
		{
			// get palm position and normal
			Leap.Vector palmNorm = hand.PalmNormal;
			Vector3 palmNormWorld = LeapUtil.LeapToWorldRot(palmNorm, handController);
			Leap.Vector palmPos = hand.PalmPosition;
			Vector3 palmPosWorld = LeapUtil.LeapToWorldPos(palmPos, handController);

			// project to figure out the distance along the normal
			Vector3 vector = position - palmPosWorld;
			Vector3 projection = Vector3.Project(vector, palmNormWorld);
			float distance = projection.sqrMagnitude;

			// debug lines
//			Debug.DrawRay(palmPosWorld,palmNormWorld, Color.red);
//			Debug.DrawRay(position, palmNormWorld, Color.blue);
//			Debug.DrawLine(palmPosWorld,palmPosWorld + projection);

			return distance;
		}

		public static float FingerCurl(Finger finger)
		{
			//Angle between metacarpal and distal phalange bones
			Vector metacarpalDirection = finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Basis.zBasis * -1f;
//			Vector metacarpalDirection = finger.Hand.PalmNormal; // experimenting with using palm instead
			Vector distalPhalangeDirection = finger.Bone(Bone.BoneType.TYPE_DISTAL).Basis.zBasis * -1f;
			float rawangle = metacarpalDirection.AngleTo(distalPhalangeDirection) * 180/Mathf.PI;
			
			//Find sign
			Vector crossBones = metacarpalDirection.Cross(distalPhalangeDirection);
			Vector boneXBasis = finger.Bone(Bone.BoneType.TYPE_METACARPAL).Basis.xBasis;
			if(finger.Hand.IsLeft) boneXBasis = boneXBasis * -1f; //Left hand uses a left-hand basis
			int sign = (crossBones.Dot(boneXBasis) >= 0) ? 1 : -1;
			return sign * rawangle;  
		}

		public static float Remap (float value, float from1, float to1, float from2, float to2) 
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}

	
		public static bool CheckPalmFacingCamera (Hand hand, HandController handController, Transform cam, float minimumDotFaceCamera)
		{
			Vector3 palmNormal = LeapUtil.LeapToWorldRot(hand.PalmNormal, handController);
			float palmCamDot = Vector3.Dot (palmNormal, cam.forward);
			if (palmCamDot < minimumDotFaceCamera)
				return true;
			else
				return false;
		}
		
		public static Hand UpdateHand(WhichHand whichHand, UIHand uiHand)
		{
			HandModel[] hands = uiHand.handController.GetAllGraphicsHands();
			if (hands.Length > 0)
			{
				for(int i=0; i < hands.Length; i++) // go through all hands in scene
				{
					Hand currentHand = hands[i].GetLeapHand(); // convert to leap hand
					if (whichHand == WhichHand.Left)
					{
						if (currentHand.IsLeft) 
							return currentHand;
						else 
							return null;
					}
					if (whichHand == WhichHand.Right)
					{
						if (currentHand.IsRight) 
							return currentHand;
						else
							return null;
					}
				}
			}
			return null;
		}
	}
}
//}
