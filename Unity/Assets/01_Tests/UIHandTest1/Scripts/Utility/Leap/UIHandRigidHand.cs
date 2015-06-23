using UnityEngine;
using System.Collections;

namespace UIHandTest1 
{
	public class UIHandRigidHand : RigidHand
	{
		// store the collider in this variable 
		// so that tools can check for collision with a hand or finger
		public GameObject currentCollider;

		public void OnChildTriggerStaySender(GameObject sender)
		{
			// sender is the bone on the finger in this case
		}

		public void OnChildTriggerStay(Collider collider)
		{
			currentCollider = collider.gameObject;
		}

		public void OnChildTriggerExit(Collider collider)
		{
			currentCollider = null;
		}
	}
}