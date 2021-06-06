//==============================================================================	
//EnemyShip Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;


public class EnemyShip : MonoBehaviour {
	//==========================================================================	
	public GameObject explosion;
	public GameObject playerShip;

	private bool inRange;
	private float moveSpeed = 1.0F; //move speed
	private float rotationSpeed = 20; //speed of turnin
	private Vector3 prevLoc;
	private Vector3 curLoc;
	
	private Transform startMarker;
	private Transform endMarker;
	public float speed = 0.5f;
	private float journeyLength;
	
	private Vector3 distance;
	
	private float distanceTravelled=0;
	private Vector3 lastPos;
	private int strafeCounter=0;	//handle direction change  0=left 1=right 2=up 3=down
	private int strafeDistance=25;	//used to control amount of strafe in a direction
	private bool firstStrafe=true;		//if first strafe double movement to compensate equal movement in each direction
	
	private bool tooFar;
	public int score=100;		//how much the enemy is worth when killed
	
	public int health = 1;	//Amount of damage the object can take before being destroyed.
	
	public int type;
	private bool firstDeath=true;
	public GameObject clone;
	public GameObject clone2;
	public bool passed=false;		//used to detect if player passed the enemy ship
//	private GameObject menu;
//	private Interface menuInter;
	public bool shot=false;
	private GameObject aiManager;
	public AudioClip hitSound;
	public AudioClip destroySound;
	private AIManager aimanagerScript;
	
	//==========================================================================\\\
	void Awake(){
		GenerateEnemy();
	}
	
	//--------------------------------------------------------------------------
	void Start(){
		//playerShip = GameObject.FindWithTag ("PlayerShip").gameObject;
		inRange = false;
//		menu = GameObject.Find ("GameManager").gameObject;
//		menuInter = menu.GetComponent<Interface> ();
		aiManager = GameObject.Find ("AIManager").gameObject;
		aimanagerScript = aiManager.GetComponent <AIManager> ();
	}
	
	//--------------------------------------------------------------------------
	void Update() {
		//if( health <= 0 ){
		//	Kill();
		//}


		if (aimanagerScript.player!=null) {		//if menuinter!=null was here
			playerShip=aimanagerScript.player;

				if (playerShip != null) {	//if menuInter.menu down and
						if (playerShip.transform != null && !inRange) {
								transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerShip.transform.position - transform.position),
		                                       rotationSpeed * Time.deltaTime);
						}
	
						if (!inRange) {
								//move towards the player
								transform.position += transform.forward * moveSpeed * Time.deltaTime;
						}
	
						if (playerShip.transform != null) {
								if (Vector3.Distance (playerShip.transform.transform.position, transform.position) > 10)
										inRange = false;
								else
										inRange = true;
		
								if (Vector3.Distance (playerShip.transform.position, transform.position) < 20)
										passed = true;
		
								if (passed && Vector3.Distance (playerShip.transform.position, transform.position) > 20)
										Destroy (this.gameObject);
						}
	
						//used for enemy stafing
						if (distanceTravelled == 0)
								lastPos = transform.position;
	
						if (firstStrafe)
								strafeCounter = RandomMovement (strafeCounter);	//function to calculate random movement for next strafe
	
						if (distanceTravelled <= strafeDistance) {
								if (strafeCounter == 0)
										transform.position += Vector3.left * moveSpeed * Time.deltaTime;
								if (strafeCounter == 1)
										transform.position += Vector3.right * moveSpeed * Time.deltaTime;
								if (strafeCounter == 2)
										transform.position += Vector3.up * moveSpeed * Time.deltaTime;
								if (strafeCounter == 3)
									transform.position += Vector3.down * moveSpeed * Time.deltaTime;
		
								distanceTravelled += Vector3.Distance (lastPos, transform.position);
						}
	
						if (distanceTravelled >= strafeDistance) {		//reset after strafe
								distanceTravelled = 0;
		
								strafeCounter = RandomMovement (strafeCounter);	//function to calculate random movement for next strafe
		
								if (firstStrafe) {
										strafeDistance += strafeDistance;	//counter to handle equal movement after first strafe
										firstStrafe = false;
								}	
						}
				}

			}
	}
	
	//--------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		//		Debug.Log (other.tag);
		if (other.tag == "PlayerBullet1") {
			ApplyDamage (5);
		} else if (other.tag == "Player") {
				shot = false;
				Kill ();	
		}

	}
	
	//--------------------------------------------------------------------------
	//Reduces health by incoming damage value.
	void ApplyDamage(int damage) {
		health -= damage;
		shot = true;
		if (health <= 0){
			Kill();
		}
	}	
	
	//--------------------------------------------------------------------------
	//Handle death animations/effects and remove object when destroyed.
	void Kill() {
		GameObject destruction;
		Vector3 newPos;
		
		GameObject enemy = this.gameObject;

		//audio.PlayOneShot (destroySound);
		destruction = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		GetComponent<AudioSource>().clip = destroySound;
		if (firstDeath && playerShip !=null) {
			newPos=transform.position;
			newPos.z+=75;
			//newPos.y+=1;
			clone= Instantiate (enemy, newPos, transform.rotation) as GameObject;
			//clone2= Instantiate (enemy, newPos, transform.rotation) as GameObject;
			clone.transform.position += Vector3.left * 50 *Time.deltaTime;
			//clone2.transform.position += Vector3.right * 50 * Time.deltaTime;
			firstDeath=false;
		}
		//shot = tru
		if (shot)
			aiManager.SendMessage ("CheckIfDead", shot);

		GetComponent<AudioSource>().Play ();
		Destroy(this.gameObject,GetComponent<AudioSource>().clip.length);
	}
	
	//--------------------------------------------------------------------------
	int RandomMovement(int strafeCounter) {
		int newCounter = 4;	//randomize strafe counter, if it equals last one, do it again (use loop)
		bool changed = false;
		int upCount = 0;
		int downCount = 0;
		int leftCount = 0;
		int rightCount = 0;
		
		while (!changed){
			newCounter=Random.Range (0,4);
			if (newCounter!=strafeCounter)
				changed=true;
		}
		
		changed = false;
		
		switch (newCounter) {	
		case 0 : leftCount++;
			if (leftCount==2) {
				newCounter=1;
				leftCount=0;
			}
			break;
		case 1 : rightCount++;
			if (rightCount==2) {
				newCounter=0;
				rightCount=0;
			}
			break;
		case 2 : upCount++;
			if (upCount==2) {
				newCounter=3;
				upCount=0;
			}
			break;
		case 3 : downCount++;
			if (downCount==2) {
				newCounter=2;
				downCount=0;
			}
			break;
		}
		
		return newCounter;
	}
	
	//--------------------------------------------------------------------------
	void GenerateEnemy() {		
		//randomly pick type of enemy
		type=Random.Range(0, 3);
		
		switch (type){			//adjust stats for specific type of enemy
			case 0 : 
				health=10;
				speed=1;
				score=100;
				break;
			case 1 : 
				health=15;
				speed=2;
				score=500;
				break;
			case 2 : 
				health=20;
				speed=4;
				score=300;
				break;
		}
	}
	
	//--------------------------------------------------------------------------
	public int Health() {
		return health;
	}
	
	//--------------------------------------------------------------------------
	public int Score () {
		return score;
	}
	
	//--------------------------------------------------------------------------
}