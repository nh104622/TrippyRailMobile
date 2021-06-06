using UnityEngine;
using System.Collections;

public class ResetCount : MonoBehaviour {

	public  int resetCount=0;
	public Interface menu;

	void Awake() {
		DontDestroyOnLoad (this);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (menu.gameOver)
						resetCount++;

	}
}
