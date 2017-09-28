using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputSet
{
	private ArrayList inputList ;
	//private Dictionary <string, InputBinder> axisList ;
	private string name;
	public bool isActive = true;

	/// <summary>
	/// Create a new Input set
	/// </summary>
	/// <param name="name">Name of the Input set</param>
	public InputSet (string name) {
		this.name = name;
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
	/// <param name="inputType">Function called when input is UP, PRESSED or DOWN</param>
	/// <param name="parameters">Parameters to be sent to the function</param>
	public void AddInput (string inputName, Action<object[]> function, InputType inputType = InputType.DOWN, params object[] parameters) {
		inputList.Add(new InputBinder(inputName, function, inputType, parameters));
	}

	/// <summary>
	/// Remove a binded function from an input
	/// </summary>
	/// <param name="inputName">Name of the input</param>
	/// <param name="function">Function to be removed</param>
	/// <param name="inputType">Remove function from UP, PRESSED or DOWN event</param>
	public void RemoveInput (string inputName, Action<object[]> function, InputType inputType = InputType.DOWN) {
		int indexToRemove = -1;
		for (int i = 0; i < inputList.Count; ++i){
			InputBinder inputBinder = (InputBinder) inputList[i];
			if ( String.Equals(inputBinder.GetName(), inputName)
				&& inputBinder.IsThisFunction(function)
					&& inputBinder.GetInputType() == inputType ){
						indexToRemove = i;
					}
		}
		if (indexToRemove != -1)
			inputList.RemoveAt(indexToRemove);
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
