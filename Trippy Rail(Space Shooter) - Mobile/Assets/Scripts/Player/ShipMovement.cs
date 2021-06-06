//==============================================================================	
//Ship Movement Script
//
//==============================================================================
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ShipMovement : MonoBehaviour{
	//==========================================================================
	public float movementSpeed = 5.0f;
	public bool inverted = true;
	public float depth = 5.0f;
    public float boostMultiplier = 2;
    

	//==========================================================================
	void Start(){
		GetComponent<Rigidbody>().freezeRotation = true;
	}

    float speed = 10.0f;
    void Update()
    {
        Vector3 dir = Vector3.zero;

        // we assume that the device is held parallel to the ground
        // and the Home button is in the right hand

        // remap the device acceleration axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(dir * speed);
    }



    //--------------------------------------------------------------------------
    /*void FixedUpdate (){
        //	float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        //	float vertical = Input.GetAxis("Vertical") * movementSpeed;

        //access joystick
        Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0) * movementSpeed;

        

        GetComponent<Rigidbody>().AddForce(moveVec);

		//if(inverted){
			//vertical *= -1;
		//}

		//testing mouse movement

	

		//finished block


		
		//GetComponent<Rigidbody>().velocity = new Vector3(horizontal,vertical,0);
	}
    */
    //--------------------------------------------------------------------------
}