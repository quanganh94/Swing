using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIScript : MonoBehaviour {

	public static int initialScore;
	int HighScore;

	public Text initialScoreText;
	public Text HighScoreText;
	public Text recentScoreText;
	public static bool isShow=false;
	public CanvasGroup CG;
	public CanvasGroup ExitMenu;
	// Use this for initialization
	void Start () {
		HighScore=PlayerPrefs.GetInt ("HighScore");
		initialScore=0;
		CG.alpha = 0;
		isShow = false;
		ExitMenu.alpha=0;
		
	}
	
	// Update is called once per frame
	void Update () {
		initialScoreText.text = "Score: " + initialScore.ToString ();
		if(Input.GetKeyDown(KeyCode.Escape)){
			ExitMenu.interactable=true;
			ExitMenu.blocksRaycasts=true;
			CG.blocksRaycasts=false;
			CG.interactable=false;
			CG.alpha=0;
			isShow=false;
			ExitMenu.alpha=1;
			ControllerScript.isDead=true;


		}
		if (isShow) {
			ExitMenu.blocksRaycasts=false;
			ExitMenu.interactable=false;
			CG.blocksRaycasts=true;
			CG.interactable=true;
			initialScoreText.enabled=false;
			if(initialScore>HighScore){
				HighScore= initialScore;
				PlayerPrefs.SetInt("HighScore",HighScore);
				PlayerPrefs.Save();
			}
			recentScoreText.text = "Your Score: " + initialScore.ToString ();
			HighScoreText.text = "High Score: " + HighScore.ToString ();
			CG.alpha=1;
		}

		
	}

	public void Quit(){
		Application.Quit(); 
	}
	public void Continue(){
//		Application.LoadLevel(Application.loadedLevel);
		Time.timeScale=1.0f;
	}
	public static void upScore(){
		if(!isShow) initialScore += 1;

	}
}
