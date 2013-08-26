using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {
	
	public float scaleMid = 100;
	public float sizeRange = .5f;
	
	float timeToStarKills = .1f;
	
	public Material baseMat;
    Color colour;
	Material mat;
	
	GameObject model;
	Light light;
	
	Color randStarColour() {
		colour = Color.yellow;
		colour.g = Random.value;
		return colour;
	}
	
	// Use this for initialization
	void Start () {
		mat = (Material) Instantiate(baseMat);
		
		model = transform.FindChild("Model").gameObject;
		light = transform.FindChild("Light").light;
		
		float scale = Random.Range(scaleMid *  (1 - sizeRange / 2), scaleMid *  (1 + sizeRange / 2));
		
		Vector3 size = transform.localScale;
		size *= scale;
		light.range *= scale;
		transform.localScale = size;
		
		mat.color = randStarColour();
		model.renderer.material = mat;
	}
	
	void Update(){
		timeToStarKills -= Time.deltaTime;	
	}
	
	void OnCollisionEnter(Collision col){
		if (col.gameObject.GetComponent<McGuffinOrbScript>() == null && col.gameObject.GetComponent<BlackHoleScript>() == null){
			if (col.gameObject.tag != "Player"){
				Destroy(col.gameObject);
			}
			
			else{
				col.gameObject.GetComponent<Health>().currentHealth = 0;
			}
		}
	}
}
