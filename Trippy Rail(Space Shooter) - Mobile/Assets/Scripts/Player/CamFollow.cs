//==============================================================================	
//Camera Follow Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {
	//==========================================================================
	public Transform ship;				//Object to be followed
	public Vector2 scale = new Vector2(0.4f, 0.4f);	//Damping of camera movement compared to player movement
	
	public float height = 2.0f;		//Distance above the object the camera follows at
	public float distance = 5.0f;	//Distance behind the object the camera follows at
	
	//==========================================================================
	// Update is called once per frame
	void LateUpdate ()
	{
		if(ship){
			Vector3 newPosition = ship.position;
			newPosition.x *= scale.x;
			newPosition.y = scale.y * newPosition.y + height;
			newPosition.z -= distance;
			transform.position = newPosition;
		}
	}
	//--------------------------------------------------------------------------
}