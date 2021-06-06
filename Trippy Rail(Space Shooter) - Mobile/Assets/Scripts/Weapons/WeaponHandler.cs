//==============================================================================	
//Weapon Handler Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {
	//==========================================================================	
	Transform currWpn;
	Transform newWpn;
	string defWpn="Autocannon";
	float wpnCD=5.0f;	//how long the powerup will be used for
	float currTime=0.0f;


	//==========================================================================
	void Start(){
		SetWeapon(defWpn);
	}

	void Update () {

		if (currWpn == null || currTime <=Time.time)
						SetWeapon (defWpn);


	}

	//----------------------------------------------------=----------------------
	void SetWeapon(string weapon){

		if (!currWpn) {
			newWpn = transform.Find(weapon);
			currWpn = newWpn;
			currWpn.SendMessage("Activate");
		} else if (currWpn.name!=weapon) {
			newWpn = transform.Find (weapon);
			currWpn.SendMessage("Deactivate");
			currWpn = newWpn;
			currWpn.SendMessage("Activate");


		}


		if (weapon!=defWpn) {
			//	currWpn.SendMessage ("Deactivate");
			currWpn.SendMessage ("Activate");
		}

		newWpn = transform.Find (weapon);
		if (newWpn) {
			newWpn.SendMessage ("Activate");
			currWpn = newWpn;
		}

		if (weapon!=defWpn)
			currTime = wpnCD + Time.time;

	}
	
	//--------------------------------------------------------------------------
}