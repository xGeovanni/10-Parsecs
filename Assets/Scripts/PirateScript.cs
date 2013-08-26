using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMotor))]

public class PirateScript : MonoBehaviour {
	
	public Transform laserOrigin;
	
	public float visionRange = 200;
	public float accuracy = .8f;
	
	public float minDamage = 10;
	public float damageVariance = 1;
	
	GameObject player;
	CharacterMotor cm;
	
	bool approxEqual(Vector3 a, Vector3 b){
		Vector3 difference = a - b;
		
		for(int i = 0; i < 3; i++){
			if (difference[i] > .1 || difference[i] < -.1)
				return false;
		}
		return true;
	}
	
	void Start() {
		cm = GetComponent<CharacterMotor>();
	}
	
	Vector3 Aim(Vector3 dirToPlayer){
		Vector3 aim = dirToPlayer;
		
		for(int i = 0; i < 3; i++){
			aim[i] *= Random.Range(accuracy, 2 - accuracy);	
		}
		
		return aim;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null){
			player = GameObject.FindGameObjectWithTag("Player");	
		}
		
		else{
				
			cm.vertical = 1;
			float dist = Vector3.Distance(transform.position, player.transform.position);
			Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
			
			if (dist < visionRange){
				Ray ray = new Ray(laserOrigin.position, Aim(dirToPlayer));
				cm.fire(gameObject.layer, ray, minDamage * (1 + Random.Range(0f, damageVariance)));
			}
						
			if (! approxEqual(dirToPlayer, transform.right * -1)){				
				Vector3 rotation = Quaternion.FromToRotation(transform.right * -1, dirToPlayer).eulerAngles.normalized;
				
				cm.x_rotation = rotation.x;
				cm.y_rotation = rotation.y;
				cm.z_rotation = rotation.z;
			}
			
			else{
				cm.x_rotation = 0;
				cm.y_rotation = 0;
				cm.z_rotation = 0;
				
			}
		}
	}
	
	public void die() {
		Destroy(gameObject);	
	}
}
