using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisBinder {

	/*public delegate void axisPrototype (string axisName, float axisValue);

	private class AxisEvent {
		public event axisPrototype axisBinding;
		public AxisEvent ( axisPrototype function){ axisBinding += function;}
		public void Play(string name, float value){
			if ( axisBinding != null )
				axisBinding (name, value);
		}
	}


	private string name;
	public bool isActive = false;

	public delegate void inputPrototype (string inputName);
	private Dictionary <InputType, AxisEvent> inputBindings;

	public AxisBinder (string name)
	{
		this.name = name;
		inputBindings = new Dictionary <InputType, AxisEvent> ();
	}

	public AxisBinder (string name, inputPrototype function, InputType type = InputType.DOWN)
		: this (name)
	{
		AddFunction (function, type);
	}

	public void AddFunction (inputPrototype function, InputType type = InputType.DOWN)
	{
		InputEvent inputEvent;
		if (inputBindings.TryGetValue (type, out inputEvent))
			inputEvent.inputBinding += function;
		else
			inputBindings.Add (type, new InputEvent(function));
	}

	public void RemoveFunction (inputPrototype function, InputType type = InputType.DOWN)
	{
		InputEvent inputEvent;
		if (inputBindings.TryGetValue (type, out inputEvent))
			inputEvent.inputBinding -= function;
	}

	public string GetName ()
	{
		return name;
	}

	public void Play (InputType type = InputType.DOWN)
	{
		InputEvent inputEvent;
		if ( inputBindings.TryGetValue (type, out inputEvent) )
			inputEvent.Play(name);
	}*/
}
