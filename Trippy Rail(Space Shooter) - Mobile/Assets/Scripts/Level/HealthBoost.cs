//==============================================================================	
//Health Booster Item Script
//
//==============================================================================
using UnityEngine;
using System.Collections;

public class HealthBoost : MonoBehaviour {
	//==========================================================================
	public int healAmt = 10;
	
	//==========================================================================
	void OnTriggerEnter(Collider other){

		if (other.name=="PlayerShip")
			other.SendMessage("Heal", healAmt);
		GetComponent<AudioSource>().Play ();
		Destroy (this.gameObject,GetComponent<AudioSource>().clip.length);
	}
	//--------------------------------------------------------------------------
}
