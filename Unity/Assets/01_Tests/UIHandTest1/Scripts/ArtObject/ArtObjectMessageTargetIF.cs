using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


namespace UIHandTest1 
{
	public interface ArtObjectMessageTargetIF : IEventSystemHandler
	{
		// functions that can be called via the messaging system
		void Select();
		void Delete();
	}
}
