using UnityEngine;
using System.Collections;
using Leap;

public class HandUI : MonoBehaviour 
{
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
			if (roll > 2 || roll < -2)
			{
				if (lHandSpawned == false)
					SpawnLHand();
			} else {
				lHandSpawned = false;
				Destroy (lHandSpawn);
			}
		}

		if (lHandSpawn != null)
			lHandSpawn.transform.position = LeapUtil.LeapToWorld(lHand.PalmPosition,frame.InteractionBox);

		if (rHand.IsValid)
		{
			float roll = rHand.PalmNormal.Roll;
			if (roll > 2 || roll < -2)
			{
//				print ("right upside down");
			}
		}
	}

	void SpawnLHand()
	{
		lHandSpawned = true;
		Vector3 pos = LeapUtil.LeapToWorld(lHand.PalmPosition,frame.InteractionBox);
		Debug.Log (pos);
		lHandSpawn = Instantiate(Resources.Load ("thing"),pos,Quaternion.identity) as GameObject;
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
