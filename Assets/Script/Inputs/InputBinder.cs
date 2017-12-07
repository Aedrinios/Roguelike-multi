using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputBinder
{
	protected string name;
	protected InputType inputType;
	private object[] parameters;
	private Action<object[]> function;

	public InputBinder (string name, Action<object[]> function, InputType inputType = InputType.DOWN, params object[] parameters) {
		this.name = name;
		this.inputType = inputType;
		this.parameters = parameters;
		this.function = function;
	}

	public bool IsThisFunction (Action<object[]> function) {
		return this.function == function;
	}

	public virtual void PlayBindedFunction () {
		if ( function != null )
			function(this.parameters);
	}

	public virtual bool IsBeingCalled(string inputSetName){
		string inputCalled = inputSetName + " " + this.name;
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
