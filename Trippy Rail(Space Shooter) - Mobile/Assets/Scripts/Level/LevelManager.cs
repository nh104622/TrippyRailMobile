//==============================================================================	
//Level Manager Script
//
//==============================================================================
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	//==========================================================================
	public Transform[] prefab;
	public int numberOfObjects = 10;
	public float recycleOffset = 12;
	public Vector3 startPosition;
	public bool start = true;

	public GameObject player;

	private ForwardMovement playerScript;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	//==========================================================================
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++) {
			objectQueue.Enqueue((Transform)Instantiate(prefab[i]));
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Recycle();
			
		}

		playerScript = player.GetComponent<ForwardMovement>();

		if (start){
			objectQueue.Dequeue();
			start = false;
		}
	}

	//--------------------------------------------------------------------------
	// Update is called once per frame
	void Update () {
		if(objectQueue.Peek().localPosition.z + recycleOffset < playerScript.DistanceTraveled()){
			Recycle();
		}
		
	}

	//--------------------------------------------------------------------------
	private void Recycle(){
		Vector3 position = nextPosition;
		position.z += 100.0f;
		
		Transform o = objectQueue.Dequeue();
		o.localPosition = position;
		objectQueue.Enqueue(o);
		
		nextPosition.z += 100.0f;
	}

	//--------------------------------------------------------------------------
}
