using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class MainMenu : MonoBehaviour {


    [System.Serializable]
    public class HighScore
    {
        public Text score;
        public Text name;

    };


	//public Interface inter;
	private int totalScores = 8;	//total amount of people on the highscore list



    //public Highscore s = new Highscore();
 
    public HighScore[] Scores;
    private static int plays;
	public GameObject uiMenu;
   // public GameObject scorePos; // used to get the position of the score on highscore table
    


	void Start () {

        Advertisement.Initialize("1036641");    //setup

        if (PlayerPrefs.GetInt("plays")==4)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
            
            plays = 0;
            PlayerPrefs.SetInt("play", plays);
        }

        if (PlayerPrefs.HasKey("confirmed"))
            uiMenu.SetActive(false);
        else
            uiMenu.SetActive(true);

        HScore();
	}
	

	void Update () {
	
	}

	void HScore() {
        

		if (!Interface.hScore) {                 //if highscores are up

           

        
			//Draw the elements
			//int counter=1;


          
          //  HighScore h = new HighScore();

           /* for (int i = 0; i < totalScores; i++){

             
              //  n.text = PlayerPrefs.GetString(i + "HScoreName");
               // s.text = PlayerPrefs.GetInt(i + "HScore").ToString();
                //h.name = n;
                //h.score = s;
                //Scores[i] = h;

              
				//Right justify
				counter++;
				
	
			}
			*/
	
			//if (GUI.Button (new Rect (Screen.width/2+100, Screen.height/2+150, 150, 100), "Go back")) {
			//	highScoresUp=false;
			//	if (reset) {
			//		reset=false;
			//		Application.LoadLevel (0);
			//	}
			//}
		}		


	}

	public void ConfirmedInstructions() {
		
		PlayerPrefs.SetString ("confirmed", "confirmed");
		
	}

	public void OnHighScore() {
		Interface.hScore = true;
	}

	public void OffHighScore() {
		Interface.hScore = false;
	}


	public void PlayGame() {
        plays++;
        PlayerPrefs.SetInt("plays", plays);
        SceneManager.LoadScene("Scene"); ;
	}

	public void Quit() {
        Application.Quit();
	}

}
