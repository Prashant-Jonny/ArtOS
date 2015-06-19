using UnityEngine;
using System.Collections;

public class UIFollowCamera : MonoBehaviour {

    public Transform cam;
    public Transform target;

    void Start ()
    {

    }

	void Update () 
    {
//        if (cam == null || targetPos == null)
//        {
//			return;
//		}

        transform.position = target.position;
        transform.rotation = target.rotation;
	}
}
