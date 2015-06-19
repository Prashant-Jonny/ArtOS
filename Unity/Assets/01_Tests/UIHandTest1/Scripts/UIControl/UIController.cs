using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;

namespace UIHandTest1 
{
	public class UIController : MonoBehaviour 
	{
		public Transform cameraTransform; // used to figure out if palm is turned toward camera
		public Transform ui;
		public HandController handController;
		public float minimumConfidence; //.5f
		public float minimumDotFaceCamera; // 0f
	}
}