using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputBinder
{
	private class InputEvent {
		public event inputPrototype inputBinding;
		public InputEvent ( inputPrototype function){ inputBinding += function;}
		public void Play(string name){
			if ( inputBinding != null )
				inputBinding (name);
		}
	}
	private string name;
	public bool isActive = false;

	public delegate void inputPrototype (string inputName);
	private Dictionary <InputType, InputEvent> inputBindings;

	public InputBinder (string name)
	{
		this.name = name;
		inputBindings = new Dictionary <InputType, InputEvent> ();
	}

	public InputBinder (string name, inputPrototype function, InputType type = InputType.DOWN)
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
	}
}

public enum InputType{
	UP,
	DOWN,
	PRESSED
}