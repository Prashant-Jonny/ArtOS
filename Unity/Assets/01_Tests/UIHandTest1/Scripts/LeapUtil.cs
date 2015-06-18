using UnityEngine;
using System.Collections;
using Leap;

//namespace Leap { // need the leap namespace for leap specific callbacks
	public class LeapUtil
	{

		public static Vector3 LeapToWorldPos(Leap.Vector position, HandController handController)
		{
			Vector3 unityPosition = position.ToUnityScaled(false);
			return handController.transform.TransformPoint(unityPosition);
		}

		public static Vector3 LeapToWorldRot(Leap.Vector direction, HandController handController)
		{
			Vector3 unityDirection = direction.ToUnityScaled(false);
			return handController.transform.TransformPoint(unityDirection);
		}

//		public static Vector3 ToPositionVector3 (Vector position)
//		{
//			return new Vector3(position.x, position.y, -position.z);
//		}
//
//		public static Vector3 ToVector3(Vector v)
//		{
//			return new Vector3(v.x,v.y,v.z);
//		}

//		public static void LookAt (Transform t, Vector normal)
//		{
//			t.LookAt(t.position + ToPositionVector3(normal), Vector3.forward);
//		}
//
//
//		public static Vector3 LeapToWorld(Vector leapPoint, InteractionBox iBox)
//		{
//			leapPoint.z *= -1.0f; // right ahnd to left hand rule
//			Vector normalized = iBox.NormalizePoint(leapPoint, false);
//			normalized -= new Vector(0.5f, -.5f, 0.5f);//recenter origin
//		
//			Vector n = normalized * 5f; //scale
//			return new Vector3 (n.x,n.y,n.z);
//		}
//
//		public static Vector3 HandToDirection(Hand hand)
//		{
//			return new Vector3(hand.Direction.x, hand.Direction.y, -hand.Direction.z);
//		}
	}
//}
