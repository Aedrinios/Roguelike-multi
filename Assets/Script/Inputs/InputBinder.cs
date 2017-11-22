using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputBinder
{
	private string name;
	private InputType inputType;
	private object[] parameters;
	private Action<object[]> function;
	private bool isAxis;

	public InputBinder (string name, Action<object[]> function, InputType inputType = InputType.DOWN, bool isAxis = false, params object[] parameters) {
		this.name = name;
		this.inputType = inputType;
		this.parameters = parameters;
		this.function = function;
		this.isAxis = isAxis;
	}

	public bool IsThisFunction (Action<object[]> function) {
		return this.function == function;
	}

	public void PlayBindedFunction () {
		if ( function != null )
			function(this.parameters);
	}

	public bool IsBeingCalled(string inputSetName){
		string inputCalled = inputSetName + " " + this.name;
		if (isAxis)
			return Input.GetAxis(inputCalled) == 1f;
		else return VerifyInput (inputCalled);
	}

	private bool VerifyInput (string inputCalled) {
		return	( inputType == InputType.DOWN && Input.GetButtonDown(inputCalled) )
			|| 	( inputType == InputType.PRESSED && Input.GetButton(inputCalled) )
			|| 	( inputType == InputType.UP && Input.GetButtonUp(inputCalled) );
	}

	public InputType GetInputType () {
		return inputType;
	}

	public string GetName () {
		return name;
	}
}
