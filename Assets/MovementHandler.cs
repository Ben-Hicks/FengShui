using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour {

	public enum Dir {Up, Right, Down, Left, None};

	public Dir curDir;

	int [] listPressedDir = new int[4];
	int nDirCount;

	void PressDir(Dir _dir){
		listPressedDir [(int)_dir] = nDirCount;
		nDirCount++;
		curDir = _dir;

		foreach (Moveable m in Object.FindObjectsOfType<Moveable> ()) {
			m.SetGravity (_dir);
		}
	}

	void ReleaseDir(Dir _dir){
		//When releasing, find the most recent timestamped
		// direction pressed, and let that be the new current direction

		listPressedDir [(int)_dir] = -1;
		int nRecentPressed = -1;
		for (int i = (int)Dir.Up; i <= (int)Dir.Left; i++) {
			if (listPressedDir [i] > nRecentPressed) {
				nRecentPressed = listPressedDir [i];
				curDir = (Dir)i;
			}
		}
	}

	// Use this for initialization
	void Start () {
		nDirCount = 0;

		listPressedDir [(int)Dir.Up] = -1;
		listPressedDir [(int)Dir.Right] = -1;
		listPressedDir [(int)Dir.Down] = -1;
		listPressedDir [(int)Dir.Left] = -1;

		curDir = Dir.Down;
	}
	
	// Update is called once per frame
	void Update () {

		//Detect releasing a direction key
		if(Input.GetKeyUp (KeyCode.W) || Input.GetKeyUp (KeyCode.UpArrow)){
			ReleaseDir(Dir.Up);
		}

		if(Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)){
			ReleaseDir(Dir.Right);
		}

		if(Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)){
			ReleaseDir(Dir.Down);
		}

		if(Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)){
			ReleaseDir(Dir.Left);
		}
			

		//Detect pressing a direction key
		if(Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)){
			PressDir(Dir.Up);
		}

		if(Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)){
			PressDir(Dir.Right);
		}

		if(Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)){
			PressDir(Dir.Down);
		}

		if(Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)){
			PressDir(Dir.Left);
		}

	}
}
