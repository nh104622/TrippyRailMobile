using UnityEngine;
using System.Collections;

public class JoystickBasic : MonoBehaviour {
	
	public Transform joystick;           // Reference to joystick prefab
	public float speed = 10;             // Movement speed
	public bool useAxisInput = true;   // Use Input Axis or Joystick
	private float h = 0, v = 0;         // Horizontal and Vertical values
	
	// Update is called once per frame
	void Update () {
		// Get horizontal and vertical input values from either axis or the joystick.
		if (!useAxisInput) {
			h = joystick.position.x;
			v = joystick.position.y;
		}
		else {
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
		}

		// Apply horizontal velocity
		if (Mathf.Abs(h) > 0) {
			GetComponent<Rigidbody>().velocity = new Vector2(h * speed, GetComponent<Rigidbody>().velocity.y);	//these were both 2d rigids
		}
		
		// Apply vertical velocity
		if (Mathf.Abs(v) > 0) {
			GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, v * speed);	//same
		}
	}
}