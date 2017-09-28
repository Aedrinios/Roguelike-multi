using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputMannager : MonoBehaviour
{
	private static List<InputSet> inputSet = new List<InputSet>();

	public static void AddSet (InputSet set)
	{
		inputSet.Add(set);
	}

	public static void RemoveSet (InputSet set)
	{
		inputSet.Remove(set);
	}

	public static void Clear ()
	{
		inputSet.Clear();
	}

	void Update () {
		foreach (InputSet set in inputSet) {
			foreach (InputBinder inputBinder in set.GetInputs()) {
				if (  InputIsBeingUsed (inputBinder, set.GetName() + " " + inputBinder.GetName()) )
					inputBinder.PlayBindedFunction();
			}
		}
	}

	bool InputIsBeingUsed (InputBinder input, string inputCompleteName) {
		return	( input.GetInputType() == InputType.DOWN && Input.GetButtonDown(inputCompleteName) )
			|| 	( input.GetInputType() == InputType.PRESSED && Input.GetButton(inputCompleteName) )
			|| 	( input.GetInputType() == InputType.UP && Input.GetButtonUp(inputCompleteName) );
	}

}
