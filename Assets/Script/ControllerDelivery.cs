using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerDelivery : MonoBehaviour {

	public InputSet[] inputs;
	private ArrayList modifiedInputs = new ArrayList();
	public int maxKeyboardsInpus = 2;
	public int maxController = 8;

	void Start () {
		inputs = new InputSet[maxController * 3 + maxKeyboardsInpus];
		AddAllKeyboards();
		AddAllControllers();
	}

	void AddAllKeyboards() {
		for ( int i = 0; i < maxKeyboardsInpus; ++i )
			inputs[i] = new InputSet("Keyboard " + (i + 1), false);
	}

	void AddAllControllers() {
		for ( int i = 0; i < maxController * 3; ++i ){
			string controllerName = "Joystick " + (i / 3 + 1);
			if (i % 3 != 0) 
				controllerName += "-" + (i % 3);
			inputs[i + maxKeyboardsInpus] = new InputSet(controllerName, false);
		}
	}

	void Update () {
		for ( int i = 0; i < maxKeyboardsInpus; ++i)
			ListenKeyboard(i);
		for ( int i = 0; i < maxController ; ++i )
			ListenController(i);
		VerifyControllerAmount();
		NotifyManager();
	}

	void ListenKeyboard (int keyboardNumber) {
		if ( Input.GetButtonDown("Keyboard " + (keyboardNumber + 1) + " start") )
			ChangeInputState(keyboardNumber, true);
		else if ( Input.GetButtonDown("Keyboard " + (keyboardNumber + 1) + " secondary") )
			ChangeInputState(keyboardNumber, false);
	}

	void ListenController (int controllerNumber) {
			if( Input.GetKeyDown("joystick " + (controllerNumber + 1) + " button 7") )
				AddSingleController(controllerNumber);
			else if ( Input.GetKeyDown("joystick " + (controllerNumber + 1) + " button 6") )
				AddMultipleController(controllerNumber);
			else if ( Input.GetKeyDown("joystick " + (controllerNumber + 1) + " button 1") )
				RemoveController(maxKeyboardsInpus);
	}

	void AddSingleController(int controllerRequested){
		controllerRequested = controllerRequested * 3 + maxKeyboardsInpus;
		Debug.Log(controllerRequested);
		ChangeInputState (controllerRequested, true);
		ChangeInputState (controllerRequested + 1, false);
		ChangeInputState (controllerRequested + 2, false);
	}

	void AddMultipleController(int controllerRequested) {
		controllerRequested = controllerRequested * 3 + maxKeyboardsInpus;
		ChangeInputState (controllerRequested, false);
		ChangeInputState (controllerRequested + 1, true);
		ChangeInputState (controllerRequested + 2, true);
	}

	void RemoveController (int controllerRequested) {
		controllerRequested = controllerRequested * 3 + maxKeyboardsInpus;
		ChangeInputState (controllerRequested, false);
		ChangeInputState (controllerRequested + 1, false);
		ChangeInputState (controllerRequested + 2, false);
	}

	void ChangeInputState (int controller, bool newActiveState ) {
		if ( newActiveState != inputs[controller].isActive ){
			inputs[controller].isActive = newActiveState;
			AddToModifiedInputs (controller);
		}
		
	}
	void AddToModifiedInputs (int controllerModified) {
		if ( modifiedInputs.Contains(inputs[controllerModified]) )
			modifiedInputs.Remove(inputs[controllerModified]);
		else
			modifiedInputs.Add(inputs[controllerModified]);
	}

	void VerifyControllerAmount () {
		int activeControllers = 0;
		for ( int i = 0; i < inputs.Length; ++i )
			if (inputs[i].isActive)
				++activeControllers ;
		if ( activeControllers > maxController )
			RemoveSomeInputs (activeControllers - maxController);
	}

	void RemoveSomeInputs (int numberOfInputs) {
		for ( int i = 0; i < inputs.Length; ++i )
			if ( inputs[i].isActive && numberOfInputs > 0 ) {
				ChangeInputState(i, false);
				--numberOfInputs;
			}
	}

	void NotifyManager(){
		if ( modifiedInputs.Count != 0 ) {
			ArrayList copy = new ArrayList(modifiedInputs);
			GameManager.instance.ReceiveUpdatedControllers(copy);
			modifiedInputs.Clear();
		}
		
	}

}
