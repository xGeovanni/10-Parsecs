using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class Gravity : MonoBehaviour {
	
	public float gravitationalConstant = 6.67384E-11f;
	public float speed = 1f;
	public float sphereRadiusScale = 30f;
	float sphereRadius;
	Rigidbody rb;
	
	public GameObject player;
	
	public List<GameObject> primaries;
	public List<Vector3> Axes;
	
	Vector3 perpendicuar(Vector3 original){
		return new Vector3(original.y, original.x, original.z);	
	}
	
	void Start(){
		rb = gameObject.GetComponent<Rigidbody>();
		rb.useGravity = false; //Unity gravity assumes that you're on Earth, but we're in SPAAAAAAACE.	
	}

	// Use this for initialization
	public void FindSatellites (){
		
		if (GetComponent<AsteroidScript>() == null)
			player = GameObject.FindGameObjectWithTag("Player");
		
		sphereRadius = sphereRadiusScale * transform.localScale.x;
		
		foreach (Collider col in Physics.OverlapSphere(transform.position, sphereRadius)) {
			
			 if (col.gameObject != gameObject){
				Gravity gravityScript = col.gameObject.GetComponent<Gravity>();
				Vector3 distance = col.transform.position - transform.position;
				float relativeDistance = distance.magnitude / sphereRadius;
				float sizeRatio = col.transform.localScale.x / transform.localScale.x;
				
				if (gravityScript != null && sizeRatio < (1 - relativeDistance)) {
					gravityScript.primaries.Add(gameObject);
					gravityScript.Axes.Add(perpendicuar((col.transform.position - transform.position).normalized));
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate() {
		/*if (primaries.Count != Axes.Count)
			Debug.LogError("Different number of primaries and axes");*/
		
		for (int i = 0; i < primaries.Count; i++){
			if (primaries[i] != null){
				Orbit(primaries[i], Axes[i]);
			}
			
			else{
				primaries.RemoveAt(i);
				Axes.RemoveAt(i);
			}
			
		}
		if (player != null)
			Pull(player);
			
	}
	
	
	void Orbit(GameObject primary, Vector3 axis){
		transform.RotateAround(primary.transform.position, axis, speed * Time.deltaTime);
	}
	
	void Pull(GameObject satellite){
		Vector3 distance = satellite.transform.position - transform.position;
	    Vector3 direction = distance.normalized;
	    float gravitationalForce = (rigidbody.mass * satellite.rigidbody.mass * gravitationalConstant) / distance.sqrMagnitude;
	    satellite.rigidbody.AddForce( direction * gravitationalForce * speed );
		
	}
}
