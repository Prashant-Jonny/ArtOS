using UnityEngine;
using System.Collections;
using Leap;

namespace UIHandTest1 
{
public class UIHand : MonoBehaviour 
{
	public GameObject leapMotionOVRController = null;
	public HandController handController = null;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (leapMotionOVRController == null || handController == null)
			return;
		
		HandModel[] hands = handController.GetAllGraphicsHands();
		if (hands.Length > 1)
		{
			Vector3 direction0 = (hands[0].GetPalmPosition() - handController.transform.position).normalized;
			Vector3 normal0 = hands[0].GetPalmNormal().normalized;
			
			Vector3 direction1 = (hands[1].GetPalmPosition() - handController.transform.position).normalized;
			Vector3 normal1 = hands[1].GetPalmNormal().normalized;

		}
	}

}
}