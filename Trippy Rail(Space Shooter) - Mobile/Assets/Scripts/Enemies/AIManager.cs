//==============================================================================	
//AI Manager Script
//
//==============================================================================	
using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {
	//==========================================================================
	public int maxEnemies = 4;	//1 for starting enemy
	public GameObject player;
	public GameObject enemyPrefab;

//	private GameObject enemyPrefab2;
	private GameObject enemy;
	private GameObject[] enemies;
	private Transform spawn;
	private PlayerShip playerScript;
	public Interface menu;
	private int counter=0;
	private bool shotDown=false;
	private bool shot=false;
	private int[] scores;	//store the score of each enemy ship to update players score when one dies
								//using an index to find the specifics enemies score
	
	//==========================================================================
	void Start(){
		//player = GameObject.FindWithTag ("Player");
		playerScript = player.GetComponent<PlayerShip>();

		scores = new int[maxEnemies];
		
		enemies= new GameObject[maxEnemies];

//		enemyPrefab2 = enemyPrefab;
		for (int i=0; i < maxEnemies; i++) {
			spawn = GameObject.Find("Spawn"+counter).transform;
			if(spawn){
				enemy = new GameObject();
				enemy = Instantiate(enemyPrefab, spawn.position, spawn.rotation) as GameObject;
				enemy.GetComponent<EnemyShip>().playerShip = player;
				enemies[i]=enemy;
				//counter = maxEnemies;
			}
			counter++;
		}
		
		GetScores ();
	}
	
	//--------------------------------------------------------------------------
	void Update () {

				CheckIfDead (shot);
		}
	
	//--------------------------------------------------------------------------
	void CheckIfDead(bool shot) {
		GameObject tempEnemy; 
		GameObject newEnemy;
		float distance = 0.0f;
		Vector3 newPos;
		
		if (!shotDown)
			shotDown = shot;
		
		for (int i=0; i<maxEnemies; i++) {
			EnemyShip enemyScript;
			
			tempEnemy = enemies[i];					//Issue arises here

			//if (playerScript==null)
				//Debug.Log ("is null");
	

			if (tempEnemy==null && player!=null)
			{
				if (shotDown && playerScript!=null) {
					playerScript.score+=scores[i];
					shotDown=false;
				}
				
				distance=Random.Range (100.0f, 150.0f);
				newPos=player.transform.position;
				newPos.z+=distance;
				newPos.y+=1.5f;
				spawn = GameObject.Find ("Spawn"+1).transform;


				if(player!=null && spawn!=null) {
					newEnemy= Instantiate (enemyPrefab,newPos, spawn.transform.rotation) as GameObject;
					//TimeAfterDeath=Time.time + 5.0f;
					enemyScript=newEnemy.GetComponent<EnemyShip>();
					
					for (int j=i; j<maxEnemies-1; j++) { 
						enemies[j]=enemies[j+1];	//delete enemy out of the list when dead
						scores[j]=scores[j+1];
					}
					//maxEnemies--;
					enemies[maxEnemies-1]=newEnemy;
					scores[maxEnemies-1]=enemyScript.score;
				}
			}
		}
	}
	
	//--------------------------------------------------------------------------
	void GetScores() {
		EnemyShip enemyScript;
		
		for (int i=0; i<maxEnemies; i++) {
			enemy= enemies[i];
			enemyScript=enemy.GetComponent<EnemyShip>();
			scores[i]=enemyScript.score;
		}
	}
	
	//--------------------------------------------------------------------------
	bool WasShot(bool shot) {	
		if(shot){
			return true;
		}
		else{
			return false;
		}
	}
}
