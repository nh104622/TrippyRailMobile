//==============================================================================	
//Projectile Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour{
	//==========================================================================
	public float lifespan = 2.0f;  //Total time that an object is allowed to exist
	public float damage = 2.0f;
	//==========================================================================
	//Initialize
	void Start() {
		Destroy(this.gameObject, lifespan);
	}

	void Update() {


	}
	
	//--------------------------------------------------------------------------
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Destructible"){
			other.SendMessage("ApplyDamage", damage);
		}
		Destroy(this.gameObject);		
	}
	
	//--------------------------------------------------------------------------
}