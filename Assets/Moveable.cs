using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour {

	const float fGravMag = 250.0f;

	MovementHandler.Dir curDir;

	Rigidbody rb;

	public void SetGravity(MovementHandler.Dir _dir){

		curDir = _dir;

	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		SetGravity (MovementHandler.Dir.None);
	}

	void ApplyForce(){
		
		Vector3 force = Vector3.zero;
		switch (curDir) {
		case MovementHandler.Dir.Down:
			force = Vector3.down;
			break;
		case MovementHandler.Dir.Left:
			force = Vector3.left;
			break;
		case MovementHandler.Dir.Right:
			force = Vector3.right;
			break;
		case MovementHandler.Dir.Up:
			force = Vector3.up;
			break;
		case MovementHandler.Dir.None:
			break;
		default:
			Debug.LogError ("Unrecognized gravity direction");
			break;
		}
		rb.AddForce (fGravMag * force * Time.deltaTime, ForceMode.Force);
	}

	// Update is called once per frame
	void Update () {
		ApplyForce ();


	}
}
