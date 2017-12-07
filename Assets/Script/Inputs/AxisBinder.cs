using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AxisBinder : InputBinder{

	private bool isPressed = false;
	
	public AxisBinder (string name, Action<object[]> function, InputType inputType = InputType.DOWN, params object[] parameters) :
		base(name, function, inputType, parameters){
	}

	public override bool IsBeingCalled(string inputSetName){
		string inputCalled = inputSetName + " " + this.name;
			if (IsInputJustPressed(inputCalled)){
				isPressed = true;
				if (inputType == InputType.DOWN)
					return true;
			}
			else if (IsInputReleased(inputCalled)){
				isPressed = false;
					if (inputType == InputType.UP)
						return true;
			}
		return inputType == InputType.PRESSED && isPressed;
	}

	private bool IsInputJustPressed(string inputCalled){
		return Input.GetAxis(inputCalled) > 0f && !isPressed;
	}

	private bool IsInputReleased(string inputCalled){
		return Input.GetAxis(inputCalled) <= 0f && isPressed;
	}

	public override void PlayBindedFunction () {
		base.PlayBindedFunction();
		if(inputType == InputType.DOWN)
			isPressed = true;
		else if (inputType == InputType.UP)
			isPressed = false;
	}
}
