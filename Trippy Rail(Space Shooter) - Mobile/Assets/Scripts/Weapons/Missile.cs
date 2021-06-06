//==============================================================================	
//Missile Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	//==========================================================================
	public 	Rigidbody 	projectile;				//Bullet Prefab
	public 	float 		rateOfFire = 0.25f;		//Base time in seconds between projectiles
	
	private float 		elapsed;          		//Time elapsed since last projectile
	private float 		projectileSpeed = 150;
	private bool 		firing = false;	        //True if firing, false otherwise
	private bool		activated = false;
	//==========================================================================
	void Start() {
		elapsed = 0.0f;
	}
	
	//--------------------------------------------------------------------------
	void Update() {
		if(activated){
			Event();
			
			if (firing)
			{
				//Weapon cooldown time has not yet passed, increment counter and wait
				if (elapsed >= 0.0f){    
					elapsed -= Time.deltaTime;
				}
				//Weapon cooldown completed, launch new projectile
				else{
					Rigidbody clone;
					clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
					clone.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
					
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
		if (Input.GetButtonUp("Fire1")){
			firing = false;
		}
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
}
