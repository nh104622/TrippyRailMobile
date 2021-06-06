//==============================================================================	
//Shotgun Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour {
	//==========================================================================
	public 	Rigidbody 	pellet;				//Bullet Prefab
	public 	float 		rateOfFire = 1.0f;	//Base time in seconds between pellets
	public	float 		pelletSpeed = 125.0f;	
	public	int			pelletCount = 2;	//Number of pellets fired per shot

	private float 		elapsed;          	//Time elapsed since last pellet
	private bool 		firing = false;	    //True if firing, false otherwise
	private bool		activated = false;
	public AudioClip shotSound;

	//==========================================================================
	void Start() {
		elapsed = 0.0f;
	}
	
	//--------------------------------------------------------------------------
	void Update() {


        if (activated)
        {
            if (elapsed >=0.1)
            Fire();
        }

		if(activated){
            //Event();
            firing = true;
			if (firing)
			{
				//Weapon cooldown time has not yet passed, increment counter and wait
				if (elapsed >= 0.0f){    
					elapsed -= Time.deltaTime;
				}
				//Weapon cooldown completed, launch new pellet
				else{
					Fire();
					elapsed = rateOfFire;
				}
			}
		}
	}
	
	//--------------------------------------------------------------------------
	//Handles inputs for weapon
	void Event(){
		if (Input.GetButtonDown("Fire1")){
			firing = true;
			elapsed = 0.0f;
		}
		//if (Input.GetButtonUp("Fire1")){
			//firing = false;
		//}
	}
	
	//--------------------------------------------------------------------------
	void Activate(){
		activated = true;
	}
	
	//--------------------------------------------------------------------------
	void Deactivate(){
		activated = false;
	}
	
	//--------------------------------------------------------------------------
	void Fire(){
		for(int i = 0; i < pelletCount; i++){
			Vector3 torque = new Vector3(Random.Range (-200, 200),
			                             Random.Range (-200, 200),
			                             0);
			
			Rigidbody clone;
			clone = Instantiate(pellet, transform.position, transform.rotation) as Rigidbody;
			GetComponent<AudioSource>().PlayOneShot (shotSound);
			clone.velocity = transform.TransformDirection(Vector3.forward * pelletSpeed);
			clone.AddForce(torque);
		}
	}
	//--------------------------------------------------------------------------
}
