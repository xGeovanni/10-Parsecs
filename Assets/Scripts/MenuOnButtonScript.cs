using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuScript))]

public class MenuOnButtonScript : MonoBehaviour {
	
	MenuScript ms;
	bool paused = false;
	
	void Start(){
		ms = gameObject.GetComponent<MenuScript>();
		ms.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Menu")){
			ms.enabled  = !ms.enabled; //Switch state
			
			print(paused);
			
			if (paused)
				Unpause();
			else
				Pause();
		}
	
	}
	
	void Pause(){
		Time.timeScale = 0;
		paused = true;
		Screen.lockCursor = false;
	}
	
	void Unpause(){
		Time.timeScale = 1;
		paused = false;
		Screen.lockCursor = true;
	}
}
