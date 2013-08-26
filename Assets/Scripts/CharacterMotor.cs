using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {
	
	public float vertical;
	public float x_rotation;
	public float y_rotation;
	public float z_rotation;
	
	HUDScript _HUDScript;
	Health healthScript;
	
	public float laserRange = 2000;
	
	public GameObject lineRenderer;
	public Transform LaserOrgin;
	
	public float speed = 50;
	public float rotateSpeed = 150;
	public float cooldown = .2f;
	
	bool recentlyFired = false;
	float timeToResetLineRenderer;
	
	public Vector3 rotationModifier = new Vector3(0, 1, 1);
	
	void Start(){
		_HUDScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<HUDScript>();
		healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
	}
	
	Vector3 elementwiseMultiply(Vector3 a, Vector3 b){
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}

	
	void resetLineRenderer(){
		lineRenderer.GetComponent<LineRenderer>().SetPosition(0, Vector3.one * 99999);
		lineRenderer.GetComponent<LineRenderer>().SetPosition(1, Vector3.one * 99999);
	}
	
	public void fire(int layer, Ray ray, float damage = 0){
		
		if (recentlyFired)
			return;
		
		RaycastHit HitInfo;
		
		LayerMask layerMask = ~(1 << layer);
		
		if (Physics.Raycast(ray, out HitInfo, laserRange, layerMask)){
			lineRenderer.GetComponent<LineRenderer>().SetPosition(0, LaserOrgin.position);
			lineRenderer.GetComponent<LineRenderer>().SetPosition(1, HitInfo.point);
			
			AsteroidScript AS = HitInfo.transform.GetComponent<AsteroidScript>();
			PirateScript PS = HitInfo.transform.GetComponent<PirateScript>();
			
			if (AS != null)
				AS.die();
			else if (PS != null)
				PS.die();
			else if (HitInfo.transform.tag == "Player"){
				
				healthScript.Damage(damage);
				_HUDScript.ActivateDamageOverlay();
			}
		}
		
		else{
			lineRenderer.GetComponent<LineRenderer>().SetPosition(0, LaserOrgin.position);
			lineRenderer.GetComponent<LineRenderer>().SetPosition(1, LaserOrgin.position + ray.direction * laserRange);	
			
		}
		
		recentlyFired = true;
		timeToResetLineRenderer = cooldown;
	}

	// Update is called once per frame
	void Update(){
		
		if (recentlyFired)
			timeToResetLineRenderer -= Time.deltaTime;
		
		if (timeToResetLineRenderer < 0 && recentlyFired){
			resetLineRenderer();
			recentlyFired = false;
		}
		
		Vector3 translation = Vector3.right * -1 * vertical * Time.deltaTime * speed;
		
		/*
		 * I know what you're thinking. "Why is it 'Vector3.right * -1'? Surely you want 'Vector3.forward'?"
		 * But, you see, that does not work, while this does. The "correct" method, of course, is to find
		 * out why it doesn't work and fix that. But the is the Ludum Dare.
		 */
		
		Vector3 rotation = new Vector3(x_rotation, y_rotation, z_rotation) * Time.deltaTime * rotateSpeed;
		
		transform.Translate(translation);
		transform.Rotate(rotation);
	
	}
	
	void LateUpdate(){
		transform.rotation = Quaternion.Euler(elementwiseMultiply(transform.eulerAngles, rotationModifier));
	}
}
