
//////////////////////////////////////////////////////////////
// SidescrollControl.js
//
// SidescrollControl creates a 2D control scheme where the left
// pad is used to move the character, and the right pad is used
// to make the character jump.
//////////////////////////////////////////////////////////////

#pragma strict

@script RequireComponent( CharacterController )

// This script must be attached to a GameObject that has a CharacterController
var moveTouchPad : Joystick;


var forwardSpeed : float = 4;
var backwardSpeed : float = 4;
var jumpSpeed : float = 16;
var inAirMultiplier : float = 0.25;					// Limiter for ground speed while jumping

private var thisTransform : Transform;
private var character : CharacterController;
private var velocity : Vector3;						// Used for continuing momentum while in air
private var canJump = true;

function Start()
{
	// Cache component lookup at startup instead of doing this every frame		
	thisTransform = GetComponent( Transform );
	character = GetComponent( CharacterController );	

	// Move the character to the correct start position in the level, if one exists
	var spawn = GameObject.Find( "PlayerSpawn" );
	if ( spawn )
		thisTransform.position = spawn.transform.position;
}

function OnEndGame()
{
	// Disable joystick when the game ends	
	moveTouchPad.Disable();	
	//jumpTouchPad.Disable();	

	// Don't allow any more control changes when the game ends
	this.enabled = false;
}

function Update()
{
	var movement = Vector3.zero;

	// Apply movement from move joystick
	if ( moveTouchPad.position.x > 0 )
		movement = Vector3.right * forwardSpeed * moveTouchPad.position.x;
	else
		movement = Vector3.left * forwardSpeed * moveTouchPad.position.x;
	
	if ( moveTouchPad.position.y > 0 )
		movement = Vector3.up * forwardSpeed * moveTouchPad.position.y;
	else
		movement = Vector3.down * forwardSpeed * moveTouchPad.position.y;

		
	movement += velocity;	
	movement += Physics.gravity;
	movement *= Time.deltaTime;
	
	// Actually move the character	
	character.Move( movement );
	
	if ( character.isGrounded )
		// Remove any persistent velocity after landing	
		velocity = Vector3.zero;
}