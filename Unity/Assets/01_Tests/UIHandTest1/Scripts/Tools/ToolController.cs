using UnityEngine;
using System.Collections;

namespace UIHandTest1 
{
	public class ToolController : MonoBehaviour 
	{
		public Transform cameraTransform; // used to figure out if palm is turned toward camera
		public HandController handController;
		public float minimumConfidence; //.5f
		public float minimumDotFaceCamera; // 0f
	}
}
