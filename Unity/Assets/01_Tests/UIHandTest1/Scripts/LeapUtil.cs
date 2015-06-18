﻿using UnityEngine;
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
			Vector3 unityDirection = direction.ToUnity(false);
			return handController.transform.TransformDirection(unityDirection);
		}

		public static float FingerCurl(Finger finger)
		{
			//Angle between metacarpal and distal phalange bones
			Vector metacarpalDirection = finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE).Basis.zBasis * -1f;
			Vector distalPhalangeDirection = finger.Bone(Bone.BoneType.TYPE_DISTAL).Basis.zBasis * -1f;
			float rawangle = metacarpalDirection.AngleTo(distalPhalangeDirection) * 180/Mathf.PI;
			
			//Find sign
			Vector crossBones = metacarpalDirection.Cross(distalPhalangeDirection);
			Vector boneXBasis = finger.Bone(Bone.BoneType.TYPE_METACARPAL).Basis.xBasis;
			if(finger.Hand.IsLeft) boneXBasis = boneXBasis * -1f; //Left hand uses a left-hand basis
			int sign = (crossBones.Dot(boneXBasis) >= 0) ? 1 : -1;
			return sign * rawangle;  
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
