using UnityEngine;
using System.Collections;

namespace UIHandTest1 
{
	public class UIHandRigidHand : RigidHand
	{
		// store the collider in this variable 
		// so that tools can check for collision with a hand or finger
		public GameObject currentCollider;

		public void OnChildCollisionEnterSender(GameObject sender)
		{
			// sender is the bone on the finger in this case
		}

		public void OnChildCollisionEnter(Collision collision)
		{
			currentCollider = collision.gameObject;
		}


		public void OnChildCollisionStaySender(GameObject sender)
		{
			// sender is the bone on the finger in this case
		}

		public void OnChildCollisionStay(Collision collision)
		{
			currentCollider = collision.gameObject;
		}

		public void OnChildCollisionExitSender(GameObject sender)
		{
			// sender is the bone on the finger in this case
		}

		public void OnChildCollisionExit(Collision collider)
		{
			currentCollider = null;
		}
	}
}