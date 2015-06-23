using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


namespace UIHandTest1 
{
	public class ArtObject : MonoBehaviour , ArtObjectMessageTargetIF
	{
		// store the collider in this variable 
		// so that tools can check for collision with a hand or finger
		public GameObject currentCollider;

		void Start () 
		{
		
		}
		
		void Update () 
		{
		
		}

		public void Select ()
		{
//			print ("select: " + gameObject.name);
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}

		public void Delete ()
		{
//			print ("delete: " + gameObject.name);
			Destroy(gameObject);
		}

		void OnTriggerEnter (Collider collider)
		{
			currentCollider = collider.gameObject;
		}

		void OnTriggerStay (Collider collider)
		{
			currentCollider = collider.gameObject;
		}

		void OnTriggerExit (Collider collider)
		{
			currentCollider = null;
		}
	}
}
