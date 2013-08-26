using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public float maxHealth = 100;
	public float healPerSecond = 0;
	
	public float currentHealth;

	// Use this for initialization
	void Start () {
		if (currentHealth == 0)
			currentHealth = maxHealth;
	}
	
	public void Damage(float amount){
		currentHealth -= amount;
	}
	
	public float heathAsRatioToMax(){
		return currentHealth / maxHealth;
	}
	
	void Update(){
		if (currentHealth <= 0){
			GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
			gameController.GetComponent<HUDScript>().dead = true;
			Screen.lockCursor = false;
			
			if (Input.anyKeyDown)
				Application.LoadLevel (0);
		}
		
		currentHealth += healPerSecond * Time.deltaTime;
	}
}
