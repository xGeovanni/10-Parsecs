using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMotor))]

public class SpaceshipScript : MonoBehaviour {
	public float mouseSensitivity = .5f;
	
	public int xAxis = 1;
	public int yAxis = -1;
	
	public int sphereCount = 0;
	
	CharacterMotor cm;
	
	void Start(){
		cm = GetComponent<CharacterMotor>();
	}
	
	void Update() {
		
		if (Input.GetMouseButtonDown(0)){
			Screen.lockCursor = true;
			
			cm.fire(gameObject.layer, Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f)));
		}
		
		cm.vertical = Input.GetAxis("Vertical");
		
		cm.y_rotation = Input.GetAxis("Mouse X") * xAxis * mouseSensitivity;
		cm.z_rotation = Input.GetAxis("Mouse Y") * yAxis * mouseSensitivity;
	}
		
}