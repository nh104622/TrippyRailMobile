//==============================================================================	
//Interface Script
//
//==============================================================================
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.SceneManagement;


public class Interface : MonoBehaviour {
	//==========================================================================
	private GameObject prefab;
	public Texture menu;
	public Texture menu2;
	public Camera menuCamera;
	public bool menuDown=false;
	private bool instructionsUp;
	public bool highScoresUp=false;
	public PlayerShip playerScript;
	private int totalScores=15;
	public GameObject player;
	private int playerScore;
	private string playerName;
	public bool gameOver=false;
	public bool lost=false;
	private string enterName;
	public static bool reset=false;
    public static int playCount = 0;    //used to count how many times user plays before ads
    //	private bool creditsUp=false;
    //	private static bool started=true;
    //	private bool gameOverMenuDown=false;
    //private ResetCount resetCount;
    //public GameObject Reset;

    public GameObject highScript;

	private static int resetCount;
	public int currReset=0;
   

	
	public GameObject uiMenu;	//used for first timers instructions menu
	public GameObject GameOver;
	public GameObject highScore;
	public static bool hScore=false;	//if highscore is up or not

    public static int currLevel = 0;



	//==========================================================================
	void Start () {


        Time.timeScale = 1.0f;
        Advertisement.Initialize("1036641");    //setup

        if (PlayerPrefs.HasKey("playCount"))
            playCount = PlayerPrefs.GetInt("playCount");

      //  Screen.orientation = ScreenOrientation.LandscapeLeft;   //flip screen for mobile platforms

		//PlayerPrefs.DeleteAll ();
		//AddDefaultScores ();
		//lost = false;
	//	enterName = "";
	//	Time.timeScale = 0.0f;
		//resetCount = Reset.GetComponent<ResetCount> ();
	
		//if (resetCount==0)
		//	started = true;
		//if (PlayerPrefs.HasKey ("confirmed"))
		//	confirmed = PlayerPrefs.GetString ("confirmed");
		//else
		//	uiMenu.SetActive (true);
		//if (PlayerPrefs.HasKey ("menuOption"))
		//	highScoresUp = true;

	}
	
	//--------------------------------------------------------------------------
	void Update () {


		if (playerScript != null)
			playerScore = playerScript.score;

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "MainMenu")
            Application.Quit();
        
        if (hScore && SceneManager.GetActiveScene().name == "MainMenu")
            highScore.SetActive(true);
        		
		if (playerScript == null && SceneManager.GetActiveScene().name =="Scene") {
			if (HighScore()) {
                //gameOver=true;
               
				highScore.SetActive(true);
			}
			else { 
				//lost=true;
				if (GameOver)
					GameOver.SetActive(true);

			}

		}
	}


	//public void ConfirmedInstructions() {

	//	PlayerPrefs.SetString ("confirmed", "confirmed");

	//}

	public void PlayGame() {
        reset = true;
        playCount++;
        PlayerPrefs.SetInt("playCount",playCount);

        if (PlayerPrefs.GetInt("playCount") == 4)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
		SceneManager.LoadScene("Scene");
        
	}

	public void Unpause() {
		Time.timeScale = 1.0f;
	}

	public void ReturnMainMenu() {
        SceneManager.LoadScene("MainMenu"); ;
	}


	public void CharacterField(string inputFieldString) {
		
		//charText.text = inputFieldString;
		playerName = inputFieldString;
	}
	
	public void Submit() {
		
		if (playerName.Length >= 1) {
			AddScore(playerName,playerScore);
			//hScore=true;
            highScript.GetComponent<HighScore>().AddNewHighscore(playerName, playerScore);
            //PlayerPrefs.SetString("menuOption","highscore");
            StartCoroutine("Wait");
			//highScoresUp=true;
			//menuCamera.camera.enabled=true;
			//menuDown=false;
			//gameOver=false;
			//reset=true;
			//gameOverMenuDown=true;
		}
		
	}

   IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        hScore = true;
        currLevel = 0;
        SceneManager.LoadScene("MainMenu");

    }

/*	public void ResetGame() {


		reset=true;
		resetCount++;
		Application.LoadLevel (0);

	}
	*/



	//--------------------------------------------------------------------------
	bool HighScore() {
		
		bool highScore = false;
		
		for (int i=0; i<totalScores; i++) {
			if (playerScore>=PlayerPrefs.GetInt (i+"HScore"))
				highScore=true;
		}
		
		return highScore;
	}

	//--------------------------------------------------------------------------
	void AddDefaultScores () {
		AddScore ("Bob", 1000);
		AddScore ("Joe", 300);
		AddScore ("Tom", 5000);
		AddScore ("Eddie", 800);
		AddScore ("Nick", 9999);
		AddScore ("Lisa", 2000);
		AddScore ("Marissa", 1800);
		AddScore ("Mike", 4500);
		AddScore ("Tommy", 7323);
		AddScore ("Sara", 900);
		AddScore ("John", 10000);
		AddScore ("Alyssa", 3000);
		AddScore ("Joe", 1500);
		AddScore ("Eric", 3400);
		AddScore ("Shane", 999);
	}

	//--------------------------------------------------------------------------
	public void AddScore(string name, int score){
		int newScore;
		string newName;
		int oldScore;
		string oldName;
		

		newScore = score;
		newName = name;

        
		for(int i=0;i<totalScores;i++){
			if(PlayerPrefs.HasKey(i+"HScore")){
				if(PlayerPrefs.GetInt(i+"HScore")<newScore){ 
					// new score is higher than the stored score
					oldScore = PlayerPrefs.GetInt(i+"HScore");
					oldName = PlayerPrefs.GetString(i+"HScoreName");
					PlayerPrefs.SetInt(i+"HScore",newScore);
					PlayerPrefs.SetString(i+"HScoreName",newName);
					newScore = oldScore;
					newName = oldName;
				}
			}else{
				PlayerPrefs.SetInt(i+"HScore",newScore);
				PlayerPrefs.SetString(i+"HScoreName",newName);
				newScore = 0;
				newName = "";
			}
		}
	}

	//--------------------------------------------------------------------------
	void OnGUI() {
		//Menu Buttons



		/*if (resetCount > 0) {
			menuCamera.camera.enabled=false;
			menuDown = true;
			Time.timeScale = 1.0f;
		}


		if(!menuDown && !instructionsUp && !highScoresUp && !gameOver && !creditsUp) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menu);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),menu2);

			//Start Game
			if (GUI.Button (new Rect (Screen.width/2+350, Screen.height/2-250, 300, 100), "Play Game")) {
				menuCamera.camera.enabled = false;
				//project story
				menuDown = true;
				//if (reset) {
				//	reset=false;
					//Application.LoadLevel (0);
				//}
				Time.timeScale = 1.0f;
			}
			if (GUI.Button (new Rect (Screen.width/2+350, Screen.height/2-150, 300, 100), "View High Scores"))
				highScoresUp=true;
			if (GUI.Button (new Rect (Screen.width/2+350, Screen.height/2-50, 300 ,100), "Instructions"))
				instructionsUp=true;
			if (GUI.Button (new Rect (Screen.width/2+350, Screen.height/2+50, 300, 100), "Credits"))
				creditsUp=true;
			if (GUI.Button (new Rect (Screen.width/2+350, Screen.height/2+150, 300, 100), "Quit"))
				Application.Quit();

		}

		if (creditsUp) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menu);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),menu2);
			GUI.Box (new Rect (Screen.width/2-300, Screen.height/2-200, 600, 250), "Credits");
			GUI.Label (new Rect (Screen.width/2-300, Screen.height/2-150, 600, 250), "Developed and Produced by Nick Huff, " 
			           + "Charles Panighetti, David Corapi");

			if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2-50, 100, 50), "Go Back"))
				creditsUp=false;



		}
*/
		//Player Destroyed
	/*	if (gameOver) {
			GUI.Box (new Rect (Screen.width/2-300, Screen.height/2-200, 700, 400), "Game Over");
			GUI.Label (new Rect (Screen.width/2-300, Screen.height/2-150, 300, 100), "New High Score!");
			GUI.Label (new Rect (Screen.width/2-300, Screen.height/2-100, 300, 100), "Enter name here...==> ");
			
			playerName="";

			enterName= GUI.TextField(new Rect (Screen.width/2-250, Screen.height/2-50, 200, 50), enterName,12);
			//GUI.Label (new Rect (650,250,200,50),playerName);
			playerName= enterName;
			if (GUI.Button (new Rect (Screen.width/2-250, Screen.height/2+50, 100, 50), "Submit")) {
				AddScore(playerName, playerScore);
				highScoresUp=true;
				menuCamera.camera.enabled=true;
				menuDown=false;
				gameOver=false;
				reset=true;
				gameOverMenuDown=true;

			}
			
			if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2+50, 100, 50), "Play Again?")) {
				//Application.LoadLevel(0);
				gameOver=false;
				//menuDown=true;
				reset=true;
				resetCount++;
				Application.LoadLevel (0);
				//menuCamera.camera.enabled = false;
				//project story
				//menuDown = true;

			}

		}
*/
	/*	if (lost) {
			GUI.Box (new Rect (Screen.width/2-300, Screen.height/2-200, 500, 300), "Game Over");
			GUI.Label (new Rect (Screen.width/2-300, Screen.height/2-150, 300, 100), "Sorry you did not make the high score table");
			if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2-50, 100, 50), "Play Again?")){
				//reset=true;
				lost=false;
				resetCount++;
				Application.LoadLevel(0);
				//menuCamera.camera.enabled = false;
				//project story
				//menuDown = true;
			}
		}
		*/

		//Instructions Screen
	/*	if (instructionsUp) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menu);
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),menu2);
			instructionsUp=true;
			GUI.Box (new Rect (Screen.width/2-300, Screen.height/2-200, 600, 300), "Instructions");
			GUI.Label (new Rect (Screen.width/2-300, Screen.height/2-150, 600, 300), "Welcome Space Pilots to Trippy, Your mission is to "
			           + "take down as many alien space ships as you can to prevent them from destroying earth! "
			           + "To play, you must use your arrow keys (or W,A,S,D) to navigate up, down, left, and right in the "
			           + "game. Use your space key to fire your weapon at the enemies. Collect as many powerups "
			           + "as you can to help you navigate through space and dominate those aliens! Yellow ones are "
			           + " hp boost and purple are weapon powerups Avoid colliding with their ships as they will do " 
			           + "any means necessary to destroy you! Watch out for solid terrain objects that could take you " 
			           + "out as well! To pause the game press k and press p to unpause. Use q to speed up. " 
			           + "and use e to slow down.  Press r to rotate your ship sideways and press t to rotate back "
			           + "Become the best pilot by killing as many enemies as you can to reach the highest score and "
			           + "receive our champion medal for your victory! But most of all, have fun and win cadet!");

			if (GUI.Button (new Rect (Screen.width/2-200, Screen.height/2, 100, 50), "Go Back"))
				instructionsUp=false;
		}
		*/

		//High Scores Screen
		/*if (highScoresUp) {
			//GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), menu);
			//GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height),menu2);
			GUILayout.BeginVertical("box");
			//Title row
			GUILayout.Label("", GUILayout.Height (50));
			GUILayout.BeginHorizontal("box");
			//Display the titles
			GUILayout.Label ("",GUILayout.Width (250));
			GUILayout.Label("Rank", "button", GUILayout.Width(120));
			GUILayout.Label("Name", "button", GUILayout.Width(220));
			GUILayout.Label("Score", "button", GUILayout.Width(120));
			GUILayout.EndHorizontal();
			//Draw the elements
			int counter=1;
			for(int i = 0; i < totalScores; i++){
				GUILayout.BeginHorizontal();
				GUILayout.Label ("", GUILayout.Width (220));
				GUILayout.Label(counter.ToString(), GUILayout.Width(120));
				GUILayout.Label(PlayerPrefs.GetString(i+"HScoreName"), GUILayout.Width(220));
				GUILayout.Label(PlayerPrefs.GetInt(i+"HScore").ToString (), GUILayout.Width (120));
				//Right justify
				counter++;
				
				GUILayout.EndHorizontal();
			}
			
			GUILayout.EndVertical();
			if (GUI.Button (new Rect (Screen.width/2+100, Screen.height/2+150, 150, 100), "Go back")) {
				highScoresUp=false;
				if (reset) {
					reset=false;
					Application.LoadLevel (0);
				}
			}
		}
		*/
	}
	//--------------------------------------------------------------------------
}
