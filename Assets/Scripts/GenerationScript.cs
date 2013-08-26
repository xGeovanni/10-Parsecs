using UnityEngine;
using System.Collections;

public class GenerationScript : MonoBehaviour {
	
	public GameObject spaceshipPrefab;
	public GameObject piratePrefab;
	public GameObject starPrefab;
	public GameObject planetPrefab;
	public GameObject blackHolePrefab;
	public GameObject asteroidPrefab;
	public GameObject mcGuffinOrbPrefab;
	
	public int neededSpheres = 5;

	
	int numStars; 
	int numPlanets;
	int numBlackHoles;
	int numAsteroids;
	int numPirates;
	public bool satellitesFound = false;
	
	public float worldSize = 3000;
	
	public GameObject spaceShip;
	public GameObject mcGuffinOrb;
	public GameObject[] stars;
	public GameObject[] planets;
	public GameObject[] blackHoles;
	public GameObject[] asteroids;
	public GameObject[] pirates;

	
	Vector3 randPos(){
		return new Vector3(Random.value, Random.value, Random.value) * worldSize;
	}
	
	// Use this for initialization
	void Start(){
		numStars = Random.Range(1, 4);
		numPlanets = Random.Range(1, 21);
		numBlackHoles = 1;
		numAsteroids = Random.Range(0, 21);
		numPirates = Random.Range(0, 100);
		
		Generate();	
	}
	
	public void Regenerate(){
		
		if (spaceShip.GetComponent<SpaceshipScript>().sphereCount >= neededSpheres){
			GetComponent<HUDScript>().won = true;
			Screen.lockCursor = false;
			return;
		}
		
		for (int i = 0; i != stars.Length; i++){
			Destroy(stars[i]);
		}
		
		for (int i = 0; i != planets.Length; i++){
			Destroy(planets[i]);
		}
		
		for (int i = 0; i != blackHoles.Length; i++){
			Destroy(blackHoles[i]);
		}
		
		for (int i = 0; i != asteroids.Length; i++){
			Destroy(asteroids[i]);
		}
		
		for (int i = 0; i != pirates.Length; i++){
			Destroy(pirates[i]);
		}
		
		numStars = Random.Range(1, 4);
		numPlanets = Random.Range(1, 21);
		numBlackHoles = 1;
		numAsteroids = Random.Range(0, 21);
		numPirates = Random.Range(0, 40);
		
		Generate(false);
		satellitesFound = false;
	}
	
	void Generate (bool newShip = true) {
		
		stars = new GameObject[numStars];
		planets = new GameObject[numPlanets];
		blackHoles = new GameObject[numBlackHoles];
		asteroids = new GameObject[numAsteroids];
		pirates = new GameObject[numPirates];
		
		for (int i = 0; i != numStars; i++){
			stars[i] = (GameObject) Instantiate(starPrefab, randPos(), Random.rotation);
		}
		
		for (int i = 0; i != numPlanets; i++){
			planets[i] = (GameObject) Instantiate(planetPrefab, randPos(), Random.rotation);
		}
		
		for (int i = 0; i != numBlackHoles; i++){
			blackHoles[i] = (GameObject) Instantiate(blackHolePrefab, randPos(), Random.rotation);
		}
		
		for (int i = 0; i != numAsteroids; i++){
			asteroids[i] = (GameObject) Instantiate(asteroidPrefab, randPos(), Random.rotation);
		}
		
		for (int i = 0; i != numPirates; i++){
			pirates[i] = (GameObject) Instantiate(piratePrefab, randPos(), Random.rotation);
		}
		
		foreach (GameObject blackHole in blackHoles){
			blackHole.GetComponent<BlackHoleScript>().generator = gameObject;
		}
		
		mcGuffinOrb = (GameObject) Instantiate(mcGuffinOrbPrefab, randPos(), Random.rotation);
		
		if (newShip){
			spaceShip = (GameObject) Instantiate(spaceshipPrefab, randPos(), Quaternion.identity);
			spaceShip.transform.LookAt(stars[Random.Range(0, stars.Length)].transform, spaceShip.transform.up);
				
		}
		
	}
	
	void Update(){
		if (! satellitesFound){
			foreach(GameObject star in stars){
				star.GetComponent<Gravity>().FindSatellites();
			}
			
			foreach(GameObject planet in planets){
				planet.GetComponent<Gravity>().FindSatellites();
			}
			
			foreach(GameObject asteroid in asteroids){
				asteroid.GetComponent<Gravity>().FindSatellites();
			}
			
			satellitesFound = true;
		}
	}
	
}
