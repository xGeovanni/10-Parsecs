using UnityEngine;
using System.Collections;

public class McGuffinOrbScript : MonoBehaviour {
	
	bool switchedToTrigger = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (! switchedToTrigger){
			collider.isTrigger = true;
			switchedToTrigger = true;
		}
	
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player"){
			col.GetComponent<SpaceshipScript>().sphereCount++;
			Destroy(gameObject);
		}
	}
}
