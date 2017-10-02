using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputSet
{
	private ArrayList inputList ;
	//private Dictionary <string, InputBinder> axisList ;
	private string name;
	public bool isActive;

	/// <summary>
	/// Create a new Input set
	/// </summary>
	/// <param name="name">Name of the Input set</param>
	/// <param name="isActive">Is the inputSet active after creation</param>
	public InputSet (string name = "", bool isActive = true) {
		this.name = name;
		this.isActive = isActive;
		inputList = new ArrayList ();
		//inputList = new Dictionary<string, AxisBinder> ();
		InputMannager.AddSet (this);
	}

	/// <summary>
	/// Empty the input set
	/// </summary>
	public void Clear () {
		inputList.Clear();
	}

	/// <summary>
	/// Bind an input to a function
	/// </summary>
	/// <param name="inputName">Name of the input</param>
	/// <param name="function">Function to bind</param>
	/// <param name="inputType">Function called when input is UP (default), PRESSED or DOWN</param>
	/// <param name="isAxis">Is the input actually an axis? (default false)</param>
	/// <param name="parameters">Parameters to be sent to the function</param>
	public void AddInput (string inputName, Action<object[]> function, InputType inputType = InputType.DOWN, bool isAxis = false, params object[] parameters) {
		inputList.Add(new InputBinder(inputName, function, inputType, isAxis, parameters));
	}

	/// <summary>
	/// Remove a binded function from an input
	/// </summary>
	/// <param name="inputName">Name of the input</param>
	/// <param name="function">Function to be removed</param>
	/// <param name="inputType">Remove function from UP, PRESSED or DOWN event</param>
	public void RemoveInput (string inputName, Action<object[]> function, InputType inputType = InputType.DOWN) {
		for (int i = 0; i < inputList.Count; ++i){
			InputBinder inputBinder = (InputBinder) inputList[i];
			if ( String.Equals(inputBinder.GetName(), inputName)
				&& inputBinder.IsThisFunction(function) )
						inputList.RemoveAt(i);
		}
	}

	/// <summary>
	/// Retrieve input list
	/// </summary>
	/// <returns>The input list if set is active</returns>
	public ArrayList GetInputs () {
		if (isActive)
			return inputList;
		return new ArrayList();
	}

	/// <summary>
	/// Retrieve input set name
	/// </summary>
	/// <returns>The name of this input set</returns>
	public string GetName(){
		return name;
	}
}
