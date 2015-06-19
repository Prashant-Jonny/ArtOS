using UnityEngine;
using System.Collections;

namespace UIHandTest1 
{
	public class UIHandRigidHand : RigidHand
	{
		public void OnChildTriggerStaySender(GameObject sender)
		{
			Debug.Log ("in a trigger");
		}

		public void OnChildTriggerStay(Collider collider)
		{
			Debug.Log ("in a trigger");
		}
	}
}