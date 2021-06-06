using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {


    const string privateCode = "TVLqf6thEU-9kyM1ij2oQwS4C6iryVjUag4A2ynQWQnQ";
    const string publicCode = "56aa8ab06e51b619c83688b9";
    const string webURL = "http://dreamlo.com/lb/";

    //DisplayHighScores highscoresDisplay;

    public Hscore[] highscoreList;

    void Awake()
    {
       // highscoresDisplay = GetComponent<DisplayHighScores>();
      //  AddNewHighscore("joe", 100);
    }

    public void AddNewHighscore(string username, int score)
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {

        
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;   //wait to return


       
    }


    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }


    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;   //wait to return

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
        }
        else
            print("Error downloading: " + www.error);
    }


   public void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Hscore[entries.Length];

        print(entries.Length);
        for (int i=0; i<entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});  //split with vertical bar
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoreList[i] = new Hscore(username, score);
            print(highscoreList[i].username + ": " + highscoreList[i].score);
            
        }

    }




}


public struct Hscore
{
    public string username;
    public int score;

    public Hscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}