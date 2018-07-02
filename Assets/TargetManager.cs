using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	public Block[] arBlocks;
	public Target[] arTargets;

	public static Color[] arColors = {Color.red, Color.blue, Color.green, Color.yellow, Color.cyan};

	public bool bSolved;

	// Use this for initialization
	void Start () {
		bSolved = false;
		arBlocks = GameObject.FindObjectsOfType<Block> ();
		arTargets = GameObject.FindObjectsOfType<Target> ();
	}

	public bool CheckFinish(){
		foreach (Target t in arTargets) {

			if (!t.isFilled()) {
				return false;
			}

		}	
		return true;
	}

	// Update is called once per frame
	void Update () {
		if (!bSolved && CheckFinish ()) {
			Debug.Log ("YOU WIN!");
			bSolved = true;
		}
	}
}
