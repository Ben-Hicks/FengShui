using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController {

	public CollisionInfo collisions;

	public Vector2 v2Velocity;
	public Vector2 v2Accel;

	public float fModVelocity = 1.0f;
	public float fModAccel = 1.0f;

	public struct CollisionInfo{
		public bool bAbove, bBelow, bLeft, bRight;
		public Vector2 v2VelocityOld;

		public void Reset(){
			bAbove = bBelow = bLeft = bRight = false;
		}
	}

	public void UpdateVelocity(){
		v2Velocity += (fModAccel * v2Accel) * Time.deltaTime;
	}

	public void SetAccel(MovementHandler.Dir _dir){
		switch (_dir) {
		case MovementHandler.Dir.Down:
			SetAccel (Vector2.down);
			break;
		case MovementHandler.Dir.Up:
			SetAccel (Vector2.up);
			break;
		case MovementHandler.Dir.Left:
			SetAccel (Vector2.left);
			break;
		case MovementHandler.Dir.Right:
			SetAccel (Vector2.right);
			break;
		case MovementHandler.Dir.None:
			SetAccel (Vector2.zero);
			break;
		}
	}

	public void SetAccel(Vector2 _v2Accel){
		v2Accel = _v2Accel;
	}

	public void Move(Vector2 v2ToMove){
		UpdateRaycastOrigins ();
		collisions.Reset ();
		collisions.v2VelocityOld = v2Velocity;

		MoveX (ref v2ToMove);

		MoveY (ref v2ToMove);

	}

	void MoveX(ref Vector2 v2ToMove){
		float fMoveRight = (v2ToMove.x >= 0) ? 1.0f : -1.0f;

		float rayLength = Mathf.Abs (v2ToMove.x) + skinWidth;

		if (Mathf.Abs (v2ToMove.x) < skinWidth) {
			rayLength = 2 * skinWidth;
		}

		for (int i = 0; i < nHorizontalRayCount; i++) {
			Vector2 rayOrigin = (fMoveRight < 0) ? raycastOrigins.v2BotLeft : raycastOrigins.v2BotRight;
			// generate raycasts along the edge from the bottom to top
			rayOrigin += Vector2.up * (fHorizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * fMoveRight, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * fMoveRight, Color.red);
			//Debug.Log (rayOrigin + " " + i);

			if (hit) {
				if (hit.distance == 0) {
					Debug.LogError ("ERROR! YOU ARE INSIDE AN OBJECT RIGHT NOW!");
					continue;
				}

				v2ToMove.x = (hit.distance - skinWidth) * fMoveRight; // move to the collision point
				rayLength = hit.distance; // shrink raycasts so that future rays are only looking for closer objects

				//update collision flags
				if (Mathf.Sign (v2ToMove.x) == -1) {
					collisions.bLeft = true;

				} else {
					collisions.bRight = true;
				}
				v2Velocity = new Vector2 (0.0f, v2Velocity.y);
			}

		}
		transform.Translate (new Vector2(v2ToMove.x, 0.0f));
	}

	void MoveY(ref Vector2 v2ToMove){
		float fMoveUp = (v2ToMove.y >= 0) ? 1.0f : -1.0f;;

		float rayLength = Mathf.Abs (v2ToMove.y) + skinWidth;

		if (Mathf.Abs (v2ToMove.y) < skinWidth) {
			rayLength = 2 * skinWidth;
		}

		for (int i = 0; i < nVerticalRayCount; i++) {
			Vector2 rayOrigin = (v2ToMove.y < 0) ? raycastOrigins.v2BotLeft : raycastOrigins.v2TopLeft;
			// generate raycasts along the edge from the left to right
			rayOrigin += Vector2.right * (fVerticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * fMoveUp, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * fMoveUp, Color.red);

			if (hit) {
				if (hit.distance == 0) {
					Debug.LogError ("ERROR! YOU ARE INSIDE AN OBJECT RIGHT NOW!");
					continue;
				}

				v2ToMove.y = (hit.distance - skinWidth) * fMoveUp; // move to the collision point
				rayLength = hit.distance; // shrink raycasts so that future rays are only looking for closer objects

				//update collision flags
				if (Mathf.Sign (v2ToMove.y) == -1) {
					collisions.bBelow = true;

				} else {
					collisions.bAbove = true;
				}
				v2Velocity = new Vector2 (v2Velocity.x, 0.0f);
			}

		}
		transform.Translate (new Vector2(0.0f,v2ToMove.y));
	}

	// Use this for initialization
	override public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateVelocity ();
		Move (fModVelocity * v2Velocity * Time.deltaTime);
	}
}
