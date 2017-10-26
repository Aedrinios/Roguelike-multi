using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {


	InputSet inputs;
	Character player;
	Vector2 direction;
	string XaxisName = "X axis";
	string YaxisName = "Y axis";

	void Start () { //A retirer après le prototype
		direction = new Vector2(0,0);
		player = this.gameObject.GetComponent<Character> ();
		BindInputs ();
	}

	void Update () {
		if (inputs != null) {
			direction.x = Input.GetAxis (inputs.GetName() + " " + XaxisName);
			direction.y = Input.GetAxis (inputs.GetName() + " " + YaxisName);
		}
	}

	void FixedUpdate () {
		if (player != null) {
			if (direction.magnitude > 1)
				direction.Normalize ();
			player.Move (direction);
		}
	}

	public void SetPlayer (Character player) {
		this.player = player;
		BindInputs ();
	}

	public void SetInputs (InputSet inputs) {
		this.inputs = inputs;
		BindInputs ();
	}

	private void BindInputs (){
		if (inputs == null || player == null)
			return;

		object[] primaryParameters = new object[1];
		primaryParameters[0] = 0;

		object[] secondaryParameters = new object[1];
		secondaryParameters[0] = 1;
		inputs.AddInput ("primary", player.InputAction, parameters:primaryParameters);
		inputs.AddInput ("secondary", player.InputAction, parameters:secondaryParameters);
	}
}
