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
		transform.root.SendMessage("OnChildCollisionEnterSender", gameObject, options);
		transform.root.SendMessage("OnChildCollisionEnter", collision, options);
	}

	void OnCollisionStay (Collision collision)
	{	
		transform.root.SendMessage("OnChildCollisionStaySender", gameObject, options);
		transform.root.SendMessage("OnChildCollisionStay", collision, options);
	}

	void OnCollisionExit (Collision collision)
	{	
		transform.root.SendMessage("OnChildCollisionExitSender", gameObject, options);
		transform.root.SendMessage("OnChildCollisionExit", collision, options);
	}

	void OnTriggerEnter (Collider collider)
	{
		transform.root.SendMessage("OnChildTriggerEnterSender", gameObject, options);
		transform.root.SendMessage("OnChildTriggerEnter", collider, options);
	}
	
	void OnTriggerStay (Collider collider)
	{
		transform.root.SendMessage("OnChildTriggerStaySender", gameObject, options);
		transform.root.SendMessage("OnChildTriggerStay", collider, options);
	}
	
	void OnTriggerExit (Collider collider)
	{
		transform.root.SendMessage("OnChildTriggerExitSender", gameObject, options);
		transform.root.SendMessage("OnChildTriggerExit", collider, options);
	}
}
