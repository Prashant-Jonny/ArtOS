using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface ToolMessageTargetIF : IEventSystemHandler
{
	// functions that can be called via the messaging system
	void FingerButtonPressBegin(int finger);
	void FingerButtonPressEnd(int finger);
}
