using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour {

	public int nHorizontalRayCount = 4;
	public int nVerticalRayCount = 4;
	public LayerMask collisionMask;
	public const float skinWidth = 0.015f;

	[HideInInspector]
	public float fHorizontalRaySpacing;
	[HideInInspector]
	public float fVerticalRaySpacing;
	[HideInInspector]
	public BoxCollider2D collider;
	[HideInInspector]
	public RayCastOrigins raycastOrigins;


	public struct RayCastOrigins{
		public Vector2 v2TopLeft, v2TopRight, v2BotLeft, v2BotRight;
	}

	public virtual void Awake(){
		collider = GetComponent<BoxCollider2D> ();
		//SpaceRays ();
	}

	// Use this for initialization
	public virtual void Start () {
		CalculateRaySpacing ();		
	}

	public void UpdateRaycastOrigins(){
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2); //Shrink by the skinwidth on each side

		raycastOrigins.v2BotLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.v2BotRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.v2TopLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.v2TopRight = new Vector2(bounds.max.x, bounds.max.y);

	}

	public void SpaceRays(){
		Bounds bounds = collider.bounds;
		//nVerticalRayCount = (int)(bounds.size.x / raySpacing);
		//nHorizontalRayCount = (int)(bounds.size.y / raySpacing);
		//Debug.Log (bounds.size.x  + " " + bounds.size.y) ;
	}
	
	public void CalculateRaySpacing(){
		Bounds bounds = collider.bounds;
		//ensure at least two rays exist (in the corners)
		nVerticalRayCount = Mathf.Clamp (nVerticalRayCount, 2, int.MaxValue);
		nHorizontalRayCount = Mathf.Clamp (nHorizontalRayCount, 2, int.MaxValue);
	
		fHorizontalRaySpacing = (bounds.size.y - (2 * skinWidth)) / (nHorizontalRayCount - 1);
		fVerticalRaySpacing = (bounds.size.x - (2 * skinWidth)) / (nVerticalRayCount - 1);
		//Debug.Log (fHorizontalRaySpacing+ " " + fVerticalRaySpacing);
	}
}
