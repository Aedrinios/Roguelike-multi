using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputSet 
{
	private Dictionary <string, InputBinder> inputList ;
	//private Dictionary <string, InputBinder> axisList ;
	private string name;
	public bool isActive = true;

	public InputSet (string m_name)
	{
		name = m_name;
		inputList = new Dictionary<string, InputBinder> ();
		//inputList = new Dictionary<string, AxisBinder> ();
		InputMannager.AddSet (this);
	}

	public void ActivateInput (string inputName)
	{
		inputList[inputName].isActive = true;
	}

	public void DeactivateInput (string inputName)
	{
		inputList[inputName].isActive = false;
	}

	/*public void ActivateAxis (string axisName)
	{
		axisList[axisName].isActive = true;
	}

	public void DeactivateAxis (string axisName)
	{
		axisList[axisName].isActive = false;
	}*/

	public void Clear ()
	{
		inputList.Clear();
	}

	public void AddInput (string inputName, InputBinder.inputPrototype functionName, InputType type = InputType.DOWN)
	{
		if (inputList.ContainsKey(inputName))
		{
			inputList[inputName].AddFunction(functionName, type);
		}
		else
		{
			inputList.Add(inputName, new InputBinder(inputName, functionName, type));
		}
	}

	public void RemoveInput (string inputName, InputBinder.inputPrototype functionName, InputType type = InputType.DOWN)
	{
		inputList[inputName].RemoveFunction(functionName, type);
	}

	/*public void AddAxis (string axisName, InputBinder.inputPrototype functionName, InputType type = InputType.DOWN)
	{
		if (axisList.ContainsKey(axisName))
		{
			axisList[axisName].AddFunction(functionName, type);
		}
		else
		{
			axisList.Add(axisName, new InputBinder(axisName, functionName, type));
		}
	}

	public void RemoveAxis (string inputName, InputBinder.inputPrototype functionName, InputType type = InputType.DOWN)
	{
		inputList[inputName].RemoveFunction(functionName, type);
	}*/

	//Copiele dictionnaire pour renvoyer une liste d'InputBinder
	public List<InputBinder> GetInput ()
	{
		List<InputBinder> inputs = new List<InputBinder>();
		foreach (KeyValuePair<string, InputBinder> entry in inputList)
		{
			inputs.Add(entry.Value);
		}
		return inputs;
	}

	public string GetName(){
		return name;
	}
}