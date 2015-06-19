using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

namespace UIHandTest1 
{
	public class UIHand : MonoBehaviour 
	{
		public Transform cameraTransform; // used to figure out if palm is turned toward camera
		public Transform ui;
		public HandController handController;
	}
}