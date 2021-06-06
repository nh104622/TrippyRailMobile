#pragma strict
 var projectile : GameObject; // the object to instantiate
var speed : float = 20.0; // default speed
var activateRate : float = 0.5; // how often to trigger the action
internal var nextActivationTime : float; // target time
internal var target : Transform;


function Start () {
	//projectile = GameObject.FindWithTag("EnemyBullet1").gameObject;
	//tempProjectile= projectile;
	target= GameObject.FindWithTag("Player").transform;
	nextActivationTime=0.0f;
}

function Update () {

	// if the Fire1 button (default is left ctrl) is pressed and the alloted time has passed
	if (target!=null) {
		if (Time.time > nextActivationTime) { 
	   		nextActivationTime = Time.time + activateRate; // reset the timer
	   		Activate(); // do whatever the fire button controls
		}
	}
	
	

}

function Activate () {  
   // create a clone of the projectile at the location & orientation of the script's parent     
	if (target!=null) {
	   var clone : GameObject = Instantiate (projectile, transform.position, transform.rotation);
   // add some force to send the projectile off in its forward direction
   	   clone.GetComponent.<Rigidbody>().velocity = transform.TransformDirection(Vector3 (0,0,speed));
   }
   
   // ignore the collider on the object the script is on
   //Physics.IgnoreCollision(clone.collider, transform.collider);


}
