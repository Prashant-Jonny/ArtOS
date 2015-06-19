using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface UIMessageTargetIF : IEventSystemHandler
{
	// functions that can be called via the messaging system
	void FingerButtonPressBegin();
	void FingerButtonPressEnd();
}
