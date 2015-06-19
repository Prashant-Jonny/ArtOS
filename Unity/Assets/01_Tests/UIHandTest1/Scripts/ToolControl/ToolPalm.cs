using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Leap;

public class ToolPalm : MonoBehaviour , ToolMessageTargetIF
{

	void Start ()
	{
	
	}
	
	void Update () 
	{
	
	}

	public void FingerButtonPressBegin()
	{
		print ("press begin message received");
	}

	public void FingerButtonPressEnd()
	{
		print ("press end message received");

	}
}
