using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SendCollisionsTriggersToRoot : MonoBehaviour {

	SendMessageOptions options;

	void Start ()
	{
		options = SendMessageOptions.DontRequireReceiver;
	}

	void OnCollisionEnter (Collision collision)
	{
		transform.parent.parent.SendMessage("OnChildCollisionEnterSender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildCollisionEnter", collision, options);
	}

	void OnCollisionStay (Collision collision)
	{	
		transform.parent.parent.SendMessage("OnChildCollisionStaySender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildCollisionStay", collision, options);
	}

	void OnCollisionExit (Collision collision)
	{	
		transform.parent.parent.SendMessage("OnChildCollisionExitSender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildCollisionExit", collision, options);
	}

	void OnTriggerEnter (Collider collider)
	{
		transform.parent.parent.SendMessage("OnChildTriggerEnterSender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildTriggerEnter", collider, options);
	}
	
	void OnTriggerStay (Collider collider)
	{
		transform.parent.parent.SendMessage("OnChildTriggerStaySender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildTriggerStay", collider, options);
	}
	
	void OnTriggerExit (Collider collider)
	{
		transform.parent.parent.SendMessage("OnChildTriggerExitSender", gameObject, options);
		transform.parent.parent.SendMessage("OnChildTriggerExit", collider, options);
	}
}
