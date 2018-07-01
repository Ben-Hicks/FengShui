using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public int nId;

	public Color color;


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

	// Use this for initialization
	void Start () {
		SetId (nId);//Initialize the Id with whatever was set in the inspector
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
