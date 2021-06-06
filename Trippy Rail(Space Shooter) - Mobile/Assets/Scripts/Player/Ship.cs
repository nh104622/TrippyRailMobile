//==============================================================================	
//Ship Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	public GameObject explosion;
	private int collisionDamage = 4;
	
	//==========================================================================	
	public int maxHealth = 10;	//Amount of damage the object can take before being destroyed.
	public int health;
	
	//==========================================================================
	void Start(){
		health = maxHealth;
		GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	//--------------------------------------------------------------------------
	void Update(){
		if( health <= 0 ){
			Kill();
		}
	}
	
	//--------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if (other.tag == "Solid"){
			ApplyDamage(collisionDamage);
		}
	}
	
	//--------------------------------------------------------------------------
	//Reduces health by incoming damage value.
	void ApplyDamage(int damage){
		health -= damage;
	}
	
	//--------------------------------------------------------------------------
	//Handle death animations/effects and remove object when destroyed.
	void Kill(){
		GameObject destruction = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		
		transform.parent.gameObject.GetComponent<CharacterController>().SendMessage("StopMoving"); 
		
		Destroy(this.gameObject);
	}
	
	//--------------------------------------------------------------------------
}