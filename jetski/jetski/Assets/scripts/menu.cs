using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public void StartGame()
	{
		
		SceneManager.LoadScene ("SampleScene");

	}

	public void quitGame()
	{
		Application.Quit ();
//		Debug.Log ("quit");
	}
}
