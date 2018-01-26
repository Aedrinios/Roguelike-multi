using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationPlatform : MonoBehaviour {
	public SpriteRenderer sprite;
	public Color startingColor, endingColor;
	public static int validatedPlatforms = 0;
	public float timeForValidation = 1.5f;
	float validationTimer;
	static bool doorsOpened = false;
	bool validated = false;
	ArrayList playersOnPlatform = new ArrayList (); 
	
	void Update () {
		validationTimer = playersOnPlatform.Count != 0 ? 
			Mathf.Min (validationTimer + Time.deltaTime, timeForValidation) :
			Mathf.Max(validationTimer - Time.deltaTime, 0);
		sprite.color = Color.Lerp(startingColor, endingColor, validationTimer / timeForValidation);
		if (validationTimer >= timeForValidation && !validated){
			++validatedPlatforms;
			validated = true;
		}
		else if (validationTimer < timeForValidation && validated){
			--validatedPlatforms;
			validated = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !doorsOpened)
			playersOnPlatform.Add(other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player" && !doorsOpened)
			playersOnPlatform.Remove(other.gameObject);
	}

	public static void DoorsOpened(){
		doorsOpened = true;
	}
}
