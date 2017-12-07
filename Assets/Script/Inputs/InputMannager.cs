using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputMannager : MonoBehaviour
{
	private static List<InputSet> inputSets = new List<InputSet>();

	public static void AddSet (InputSet set)
	{
		inputSets.Add(set);
	}

	public static void RemoveSet (InputSet set)
	{
		inputSets.Remove(set);
	}

	public static void Clear ()
	{
		inputSets.Clear();
	}

	void Update () {
		foreach (InputSet set in inputSets) {
			set.VerifyInputs();
		}
	}

}
