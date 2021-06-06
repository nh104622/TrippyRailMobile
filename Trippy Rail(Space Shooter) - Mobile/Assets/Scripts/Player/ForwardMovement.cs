//==============================================================================	
//Forward Rail Movement Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class ForwardMovement : MonoBehaviour{
	//==========================================================================
	public GameObject ship;

	public float baseSpeed = 10.0f;		//Base forward morement speed.
	public float boostSpeedMax = 20.0f;	//Maximum speed while boosting.
	public float boostScale = 0.2f;	//Length of time from boost initialization until max speed.
	public float brakeSpeedMin = 3.0f;		//Minimum speed while braking.
	public float brakeScale = 0.2f;	//Length of time from brake initialization until min speed.
	public GameObject menuHolder;
	
	public KeyCode boostKey = KeyCode.Q;
	public KeyCode brakeKey = KeyCode.E;

	private float currSpeed;
//	private Interface menu;
	public bool boosting = false;
	private bool braking = false;

	//--------------------------------------------------------------------------
	//Speed Meter Attributes
	private Rect box = new Rect(200, 200, 200, 20);
	private Texture2D frame;
	private Texture2D background;
	private Texture2D foreground;

    private PlayerShip pScript;

    public GUIStyle statStyle;


	//==========================================================================
	void Start(){

        pScript = ship.GetComponent<PlayerShip>();
		currSpeed = baseSpeed;

//		menu = menuHolder.GetComponent<Interface>();

		frame 	   = new Texture2D(1, 1, TextureFormat.RGB24, false);
		background = new Texture2D(1, 1, TextureFormat.RGB24, false);
		foreground = new Texture2D(1, 1, TextureFormat.RGB24, false);
		frame.SetPixel(0, 0, Color.black);
		background.SetPixel(0, 0, Color.grey);
		foreground.SetPixel(0, 0, Color.yellow);
		frame.Apply();
		background.Apply();
		foreground.Apply();
	}

	//--------------------------------------------------------------------------
	void Update (){
		if(pScript.health>0){
			OnEvent();	//Handle key inputs

			if (boosting){
				if(currSpeed < boostSpeedMax){
					currSpeed += boostScale;
				}
			} 
			else if (braking) {
				if(currSpeed > brakeSpeedMin){
					currSpeed -= brakeScale;
				}
			}
			else{
				if(currSpeed > baseSpeed + 0.3f){
					currSpeed -= boostScale;
				}
				else if(currSpeed < baseSpeed - 0.3f){
					currSpeed += brakeScale;
				}
				else{
					currSpeed = baseSpeed;
				}
			}

			transform.position += transform.forward * currSpeed * Time.deltaTime;
		}
	}

	//--------------------------------------------------------------------------
	void OnEvent(){
		if(!braking && !boosting){
			if(Input.GetKeyDown(boostKey)){
				boosting = true;
			}
			else if (Input.GetKeyDown(brakeKey)) {
				braking = true;
			}
		}

		if (boosting) {
			if(Input.GetKeyUp(boostKey)){
				boosting = false;
			}
		}
		else if(braking){
			if(Input.GetKeyUp(brakeKey)){
				braking = false;
			}
		}
	}

    public void ToggleBoostButton()
    {
        if (boosting)
            boosting = false;
        else
            boosting = true;
    }

	//--------------------------------------------------------------------------
	void OnGUI() {
		//if(menu.menuDown){
			GUI.Label(new Rect(0, 70, 150, 100), "Health ",statStyle);
			GUI.DrawTexture(new Rect(0, 50, box.width + 4, box.height + 4), frame, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 50, box.width, box.height), background, ScaleMode.StretchToFill);
			GUI.DrawTexture(new Rect(0, 50, box.width*currSpeed/boostSpeedMax, box.height), foreground, ScaleMode.StretchToFill);
		//}
	}

	//--------------------------------------------------------------------------
	public float DistanceTraveled(){
		return(transform.position.z);
	}
	//--------------------------------------------------------------------------
}
