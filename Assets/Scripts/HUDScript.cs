using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {
	
	public Texture2D crosshair;
	public Texture2D damageOverlay;
	public Texture2D redHeathBar;
	public Texture2D greenHealthBar;
	public Texture2D startScreen;
	public Texture2D deathScreen;
	public Texture2D winScreen;
	
	public Health healthScript;
	
	public float damageOverlayCooldown = .4f;
	
	Rect crosshairPos;
	Rect screenRect;
	Rect healthBarRect;
	Rect greenHealthBarRect;
	
	bool drawDamageOverlay = false;
	public bool start = false;
	public bool won = false;
	public bool dead = false;
	float damageOverlayTime;
	
	float lastHealth;

	private Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
		// Jon Martin (http://jon-martin.com/) wrote this function.
		
	    Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
	    Color[] rpixels=result.GetPixels(0);
	    float incX=(1.0f / (float)targetWidth);
	    float incY=(1.0f / (float)targetHeight); 
	    for(int px=0; px<rpixels.Length; px++) { 
	        rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth), incY*((float)Mathf.Floor(px/targetWidth))); 
	    } 
	    result.SetPixels(rpixels,0); 
	    result.Apply(); 
	    return result; 
	}
	
	// Use this for initialization
	void Start () {
		crosshairPos = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) / 2, crosshair.width, crosshair.height);
		screenRect = new Rect(0, 0, Screen.width, Screen.height);
		healthBarRect = new Rect(0, 0, redHeathBar.width, redHeathBar.height);
		greenHealthBarRect = new Rect(0, 0, greenHealthBar.width, greenHealthBar.height);
		
		damageOverlay = ScaleTexture(damageOverlay, Screen.width, Screen.height);
		startScreen = ScaleTexture(startScreen, Screen.width, Screen.height);
		deathScreen = ScaleTexture(deathScreen, Screen.width, Screen.height);
		winScreen = ScaleTexture(winScreen, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update() {
		if (healthScript == null){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player != null)
				healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
		}
		if (drawDamageOverlay)
			damageOverlayTime -= Time.deltaTime;
		if (damageOverlayTime <= 0)
			drawDamageOverlay = false;
		
		if (healthScript != null && healthScript.currentHealth != lastHealth && healthScript.currentHealth > 0){
			greenHealthBar = ScaleTexture(greenHealthBar, Mathf.CeilToInt(redHeathBar.width * healthScript.heathAsRatioToMax()), redHeathBar.height);
			greenHealthBarRect = new Rect(0, 0, greenHealthBar.width, greenHealthBar.height);
			lastHealth = healthScript.currentHealth;
		}
	}
	
	void OnGUI () {
		GUI.DrawTexture(crosshairPos, crosshair);
		GUI.DrawTexture(healthBarRect, redHeathBar);
		GUI.DrawTexture(greenHealthBarRect, greenHealthBar);
		
		if (drawDamageOverlay)
			GUI.DrawTexture(screenRect, damageOverlay);
		
		if (start){
			GUI.DrawTexture(screenRect, startScreen);	
		}
		
		if (dead){
			GUI.DrawTexture(screenRect, deathScreen);	
		}
		
		if (won){
			GUI.DrawTexture(screenRect, winScreen);	
		}
	}
	
	public void ActivateDamageOverlay(){
		drawDamageOverlay = true;
		damageOverlayTime = damageOverlayCooldown;
	}
}
