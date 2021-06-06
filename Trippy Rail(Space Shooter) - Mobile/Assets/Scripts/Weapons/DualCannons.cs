//==============================================================================	
//Dual Cannons Handler Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class DualCannons : MonoBehaviour {
	//==========================================================================	
	public Transform leftCannon;
	public Transform rightCannon;
	
	//==========================================================================
	void Activate(){
		leftCannon.SendMessage("Activate");
		rightCannon.SendMessage("Activate");
	}
	
	//--------------------------------------------------------------------------
	void Deactivate(){
		leftCannon.SendMessage("Deactivate");
		rightCannon.SendMessage("Deactivate");
	}
	
	//--------------------------------------------------------------------------
}