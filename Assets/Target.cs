using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	public int nId;

	public Color color;

	public bool bFilled;

	public void SetId(int _id){
		Debug.Assert (_id < TargetManager.arColors.Length);

		nId = _id;

		SetColor (TargetManager.arColors [nId]);
	}

	void SetColor(Color _color){
		color = _color;

		Renderer render = GetComponent<Renderer> ();

		render.material.shader = Shader.Find ("_Color");
		render.material.SetColor ("_Color", _color);

		render.material.shader = Shader.Find ("Specular");
		render.material.SetColor ("_SpecColor", _color);
	}

	void OnTriggerEnter2D (Collider2D collision){
		Block collided = collision.GetComponent<Block> ();
		if (collided == null) {
			//Then we didn't collide with a block
			return;
		}

		if (collided.nId == nId) {
			// Then we're being filled by the right block
			bFilled = true;
		}
	}

	void OnTriggerExit2D (Collider2D collision){
		Block collided = collision.GetComponent<Block> ();
		if (collided == null) {
			//Then this isn't a block so we don't care
			return;
		}

		if (collided.nId == nId) {
			// Then our satisfying block just left us
			bFilled = false;
		}
	}

	public bool isFilled(){
		return bFilled;
	}

	// Use this for initialization
	void Start () {
		bFilled = false;
		SetId (nId);//Initialize the Id with whatever was set in the inspector
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
