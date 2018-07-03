using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour {

	LevelManager levelManager;

	void Start(){
		levelManager = LevelManager.Instance;
		DontDestroyOnLoad (this);
	}

	void Update () {



		if (Input.GetKeyUp (KeyCode.Return)) {
			Debug.Log ("Next Level");
			levelManager.NextLevel ();
		} else if (Input.GetKeyUp (KeyCode.Backspace)) {
			Debug.Log ("Previous Level");
			levelManager.PreviousLevel ();
		}

	}
}
