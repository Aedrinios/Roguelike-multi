using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerDelivery : MonoBehaviour {

	public InputSet[] inputs;
	public Action<InputSet> StartPressed, ReturnPressed;


	public int maxKeyboardsInpus = 2;
	public int maxController = 8;

	void Start () {
		inputs = new InputSet[maxController + maxKeyboardsInpus];
		AddAllKeyboards();
		AddAllControllers();
	}

	void AddAllKeyboards() {
		for ( int i = 0; i < maxKeyboardsInpus; ++i )
			inputs[i] = new InputSet("Keyboard " + (i + 1));
	}

	void AddAllControllers() {
		for ( int i = 0; i < maxController; ++i ){
			string controllerName = "Joystick " + (i + 1);
			inputs[i + maxKeyboardsInpus] = new InputSet(controllerName, isController:true);
		}
	}

	void Update () {
		for ( int i = 0; i < maxKeyboardsInpus; ++i)
			ListenKeyboard(i);
		for ( int i = 0; i < maxController ; ++i )
			ListenController(i);
	}

	void ListenKeyboard (int keyboardNumber) {
		if ( Input.GetButtonDown("Keyboard " + (keyboardNumber + 1) + " start") )
			StartPressed(inputs[keyboardNumber]);
		else if ( Input.GetButtonDown("Keyboard " + (keyboardNumber + 1) + " secondary") )
			ReturnPressed(inputs[keyboardNumber]);
	}

	void ListenController (int controllerNumber) {
		if( Input.GetKeyDown("joystick " + (controllerNumber + 1) + " button 7") )
			StartPressed(inputs[controllerNumber + maxKeyboardsInpus]);
		else if ( Input.GetKeyDown("joystick " + (controllerNumber + 1) + " button 1") )
			ReturnPressed(inputs[controllerNumber + maxKeyboardsInpus]);
	}
}
