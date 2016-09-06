using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	//Start the game
	public void GoToLevel(string name){
		SceneManager.LoadScene (name);
	}

	//Quit the game
	public void QuitGame(string name){
		Application.Quit ();
	}

	public void LoadScene(string name){
		SceneManager.LoadScene (name);
	}

	public void LoadNextLevel(){
		Debug.Log ("Needs to load next scene, but not doing that in this game");
	}

}
