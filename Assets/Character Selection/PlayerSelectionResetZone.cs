using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionResetZone : MonoBehaviour {

	bool isActive = true;
	float lerpTime = 0.5f, timer = 0;
	public Color begin, end;
	public SpriteRenderer spriteRenderer;

	void Update(){
		if (!isActive){
			timer += Time.deltaTime;
			spriteRenderer.color = Color.Lerp(begin, end, Mathf.Min(timer / lerpTime, 1f));
		}
	}
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player" && isActive){
			SelectCharState gameState = GameManager.instance.gameObject.GetComponent<SelectCharState>();
			gameState.RemovePlayer(other.gameObject);
		}
	}

	public void Deactivate(){
		isActive = false;
	}
}
