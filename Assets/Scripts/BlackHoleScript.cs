using UnityEngine;
using System.Collections;

public class BlackHoleScript : MonoBehaviour {
	
	public float scaleMid = 100;
	public float sizeRange = .5f;
	
	bool switchedToTrigger = false;
	
	GameObject model;
	
	public GameObject generator;

	
	// Use this for initialization
	void Start () {
		
		float scale = Random.Range(scaleMid *  (1 - sizeRange / 2), scaleMid *  (1 + sizeRange / 2));
		
		Vector3 size = transform.localScale;
		size *= scale;
		transform.localScale = size;
		
	}
	
	void Update(){
		if (! switchedToTrigger){
			collider.isTrigger = true;
			switchedToTrigger = true;
		}
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player"){
			generator.GetComponent<GenerationScript>().Regenerate();	
		}
	} 
}
