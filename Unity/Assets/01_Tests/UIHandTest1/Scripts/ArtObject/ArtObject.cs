using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ArtObject : MonoBehaviour , ArtObjectMessageTargetIF
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public void Select ()
	{
		print ("select: " + gameObject.name);
	}

	public void Delete ()
	{
		print ("delete: " + gameObject.name);
	}
}
