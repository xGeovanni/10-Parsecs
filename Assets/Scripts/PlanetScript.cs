using UnityEngine;
using System.Collections;

public class PlanetScript : MonoBehaviour {
	
	public float scaleMid = 20;
	public float sizeRange = .5f;
	
	public Material baseMat;
    Color colour;
	Material mat;
	
	GameObject model;
	
	Color randPlanetColour() {
		return new Color(Random.value, Random.value, Random.value);
	}
	
	// Use this for initialization
	void Start () {
		mat = (Material) Instantiate(baseMat);
		
		model = transform.FindChild("Model").gameObject;
		
		float scale = Random.Range(scaleMid *  (1 - sizeRange / 2), scaleMid *  (1 + sizeRange / 2));
		
		Vector3 size = transform.localScale;
		size *= scale;
		transform.localScale = size;
		
		mat.color = randPlanetColour();
		model.renderer.material = mat;
	}
}
