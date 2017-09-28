using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputBinder
{
	private string name;
	private InputType inputType;
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

	public void PlayBindedFunction (params object[] parameters) {
		if ( function != null )
			function(parameters);
	}

	public InputType GetInputType () {
		return inputType;
	}
}
