using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager> {

	public int nCurScene = 0;

	public void NextLevel(){
		nCurScene++;
		GoToScene (nCurScene);
	}

	public void PreviousLevel(){
		nCurScene--;
		GoToScene (nCurScene);
	}

	void GoToScene(int nSceneID){
		//if (nSceneID < SceneManager.sceneCount) {
			Debug.Log ("Loading Level" + nSceneID);
			SceneManager.LoadScene ("Level"+nSceneID, LoadSceneMode.Single);
		//} else {
		//	Debug.LogError ("No such level exists");
		//}
	}
}
