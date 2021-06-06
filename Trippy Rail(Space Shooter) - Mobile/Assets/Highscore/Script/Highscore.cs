using UnityEngine;
using System.Collections;
using System.Text;
using System.Security;

public class Highscore : MonoBehaviour 
{
	public string secretKey = "12345";
	public string PostScoreUrl = "http://YouWebsite.com/.../postScore.php?";
	public string GetHighscoreUrl = "http://YouWebsite.com/.../getHighscore.php";

	//private string hName = "Name";
	//private string score = "Score";
	private string WindowTitle = "Leaderboards";
	private string[] Score;

	public GUISkin Skin;
	public float windowWidth = 380;
	private float windowHeight = 300;
	public Rect windowRect;

	public int maxNameLength = 10;
	public int getLimitScore = 15;
	
	
	void Start () 
	{
        Score = new string[getLimitScore];  //max amount of entries
		windowRect = new Rect (120, 40, 300, 300);
		StartCoroutine("GetScore");		
	}

	void Update () 
	{
		windowRect = new Rect (Screen.width / 2 -(windowWidth / 2), 80, windowWidth, Screen.height - 100);
		windowHeight = Screen.height - 50;
	}
	
	IEnumerator GetScore()
	{
		
			
    	WindowTitle = "Loading Highscores...";
		
		WWWForm form = new WWWForm();
		form.AddField("limit",getLimitScore);
		
    	WWW www = new WWW(GetHighscoreUrl,form);
    	yield return www;
		
		if(www.text == "") 
    	{
			print("There was an error getting the high score: " + www.error);
			WindowTitle = "There was an error getting the high score";
    	}
		else 
		{
			WindowTitle = "Leaderboards";
            FormatHighscores(www.text);
            //Score = www.text;
		}
	}


    public void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

     
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });  //split with vertical bar
            //string username = entryInfo[0];
            //int score = int.Parse(entryInfo[1]);

            Score[i] = entryInfo[0] + " - " + entryInfo[1];

        }
    }
    


        IEnumerator PostScore(string name, int score)
	{
		string _name = name;
		int _score = score;
		
		string hash = Md5Sum(_name + _score + secretKey).ToLower();
		
		WWWForm form = new WWWForm();
		form.AddField("name",_name);
		form.AddField("score",_score);
		form.AddField("hash",hash);
		
		WWW www = new WWW(PostScoreUrl,form);
		WindowTitle = "Wait";
		yield return www;
		
    	if(www.text == "done") 
    	{
       		StartCoroutine("GetScore");
    	}
		else 
		{
			print("There was an error posting the high score: " + www.error);
			WindowTitle = "There was an error posting the high score";
		}
	}
	
    

	void OnGUI()
	{
	GUI.skin = Skin;
		
        
		windowRect = GUI.Window(0, windowRect, DoMyWindow, WindowTitle);
	
		//hName = GUI.TextField (new Rect (Screen.width / 2 - 160, 10, 100, 20), name, maxNameLength);
    	//score = GUI.TextField (new Rect (Screen.width / 2 - 50, 10, 100, 20), score, 25);
		
    	/*if (GUI.Button(new Rect(Screen.width / 2 + 60, 10, 90, 20),"Post Score"))
    	{
			StartCoroutine(PostScore(name, int.Parse(score)));
       		hName = "";
       		score = "";
    	} 
        */   
	}
	
	void DoMyWindow(int windowID) 
	{
      GUI.skin = Skin;

        int h = 60;


        GUI.Label(new Rect(windowWidth / 2 - windowWidth / 2, 30, windowWidth, windowHeight), "Name - Score");
        //prints out highscores
        for (int i = 0; i < Score.Length; i++)
        {
            GUI.Label(new Rect(windowWidth / 2 - windowWidth / 2, h, windowWidth, windowHeight), Score[i]);
            h += 30;
        }
    	
    	if (GUI.Button(new Rect(15,Screen.height - 90,70,30),"Refresh"))
    	{
			StartCoroutine("GetScore");
    	}

        if (GUI.Button(new Rect(20, Screen.height - 90, 70, 30), "Okay"))
        {
            gameObject.SetActive(false);
        }  
    }
	
	public string Md5Sum(string input)
	{
    	System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
    	byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
    	byte[] hash = md5.ComputeHash(inputBytes);
 
    	StringBuilder sb = new StringBuilder();
    	for (int i = 0; i < hash.Length; i++)
    	{
    	    sb.Append(hash[i].ToString("X2"));
    	}
    	return sb.ToString();
	}
}
