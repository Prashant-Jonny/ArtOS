using UnityEngine;
using System.Collections;
using Leap;

public class LeapCanvasMover : MonoBehaviour 
{
	public GameObject canvas;
	public float turnUpLimit;

	Controller controller;
	Frame frame;
	Hand lHand;
	Hand rHand;
		
	bool lHandSpawned = false;
	GameObject lHandSpawn;

	void Start () 
	{
		controller = new Controller();
		lHand = new Hand();
		rHand = new Hand();
	}
	
	void Update () 
	{
		frame = controller.Frame();
		HandList hands = frame.Hands;

		GetHands (hands);

		if (lHand.IsValid)
		{
			float roll = lHand.PalmNormal.Roll;
			if (roll > turnUpLimit || roll < -turnUpLimit) // if left hand turned up
			{
				MoveCanvas(lHand);
			} 
		}


		if (rHand.IsValid)
		{
			float roll = rHand.PalmNormal.Roll;
			if (roll > turnUpLimit || roll < -turnUpLimit) // if right hand turned up
			{

			}
		}
	}

	void MoveCanvas(Hand hand)
	{
		Vector3 pos = LeapUtil.LeapToWorld(hand.PalmPosition,frame.InteractionBox);
		canvas.transform.position = pos;
		canvas.transform.forward = LeapUtil.HandToDirection(hand);
	}


	void GetHands (HandList hands)
	{
		foreach (Hand h in hands)
		{
			
			if (h.IsLeft)
			{
				lHand = h;
			}
			if (h.IsRight)
			{
				rHand = h;
			}
		}
	}

}
