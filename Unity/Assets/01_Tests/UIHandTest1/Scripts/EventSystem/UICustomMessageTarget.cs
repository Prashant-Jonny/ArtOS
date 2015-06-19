using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface UICustomMessageTargetInterface : IEventSystemHandler
{
	// functions that can be called via the messaging system
	void Message1();
	void Message2();
}

public class UICustomMessageTarget : MonoBehaviour, UICustomMessageTargetInterface
{
	public void Message1()
	{
		Debug.Log ("Message 1 received");
	}
	
	public void Message2()
	{
		Debug.Log ("Message 2 received");
	}
}
