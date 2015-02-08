using UnityEngine;
using Leap;

namespace Leap {
	public class LeapUtil
	{
		public static Vector3 ToPositionVector3 (Vector position)
		{
			return new Vector3(position.x, position.y, -position.z);
		}

		public static Vector3 ToVector3(Vector v)
		{
			return new Vector3(v.x,v.y,v.z);
		}

		public static void LookAt (Transform t, Vector normal)
		{
			t.LookAt(t.position + ToPositionVector3(normal), Vector3.forward);
		}


		public static Vector3 LeapToWorld(Vector leapPoint, InteractionBox iBox)
		{
			leapPoint.z *= -1.0f; // right ahnd to left hand rule
			Vector normalized = iBox.NormalizePoint(leapPoint, false);
			normalized -= new Vector(0.5f, -.5f, 0.5f);//recenter origin
		
			Vector n = normalized * 5f; //scale
			return new Vector3 (n.x,n.y,n.z);
		}

		public static Vector3 HandToDirection(Hand hand)
		{
			return new Vector3(hand.Direction.x, hand.Direction.y, -hand.Direction.z);
		}
	}
}
