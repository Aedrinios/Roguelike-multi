using UnityEngine;
using System.Collections;

public class Timer {
	private float time = 0f;
	public float endTime;
	public delegate void TimerEnd () ;
	private event TimerEnd OnTimerEnd;
	private TimerState state = TimerState.NONE;


	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	public Timer (float endTime){
		TimerManager.SetupTimer (this);
		this.endTime = endTime;
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="function">Fonction déclanché à la fin du timer.</param>
	public Timer (float endTime, TimerEnd function) : this (endTime){
		OnTimerEnd += function;
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="isAlreadyFinished">Le timer doit-il être fini tout de suite?</param>
	public Timer(float endTime, bool isAlreadyFinished) : this (endTime){
		if (isAlreadyFinished) {
			time = endTime;
			state = TimerState.FINISHED;
		}
	}

	/// <summary>
	/// Crée un nouveau Timer
	/// </summary>
	/// <param name="endTime">Temps de fin du chrono</param>
	/// <param name="function">Fonction déclanché à la fin du timer.</param>
	/// <param name="isAlreadyFinished">Le timer doit-il être fini tout de suite?</param>
	public Timer ( float endTime, TimerEnd function, bool isAlreadyFinished) : this (endTime, isAlreadyFinished){
		OnTimerEnd += function;
	}

	public float m_time {
		get {
			return time;
		}
		set {
			if (state == TimerState.STARTED) {
				if (value >= endTime) {
					time = endTime;
					state = TimerState.FINISHED;
					if (OnTimerEnd != null)
						OnTimerEnd ();
				}
				else
					time = value;
			}
		}
	}
	/// <summary>
	/// Remet le timer à 0.
	/// /!\ Reset() ne relance pas le timer!
	/// Pour cela, il y a ResetPlay()
	/// </summary>
	public void Reset () {
		state = TimerState.NONE;
		time = 0f;
	}
	/// <summary>
	/// Pause le timer.
	/// </summary>
	public void Pause (){
		state = TimerState.NONE;
	}
	/// <summary>
	/// Détermine si ce timer est fini.
	/// </summary>
	public bool IsFinished () {
		return state == TimerState.FINISHED;
	}
	/// <summary>
	/// Détermine si ce timer est lancé ou non.
	/// </summary>
	/// <returns><c>true</c> if this instance is started; otherwise, <c>false</c>.</returns>
	public bool IsStarted() {
		return state == TimerState.STARTED;
	}
	/// <summary>
	/// Lance le timer.
	/// /!\ Cette méthode ne remet pas le timer à 0!
	/// Pour cela, il y a la méthode Reset().
	/// </summary>
	public void Play () {
		state = TimerState.STARTED;
	}

	/// <summary>
	/// Ajoute une fonction au timer
	/// </summary>
	/// <param name="newFunction">Nouvelle fonction à ajouter.</param>
	public void AddFunction (TimerEnd newFunction){
		OnTimerEnd += newFunction;
	}

	/// <summary>
	/// Retire une fonction de ce timer.
	/// </summary>
	/// <param name="oldFunction">Fonction à retirer.</param>
	public void RemoveFunction (TimerEnd oldFunction){
		OnTimerEnd -= oldFunction;
	}

	/// <summary>
	/// Reset et lance le timer
	/// </summary>
	public void ResetPlay(){
		Reset ();
		Play ();
	}

	public float m_endTime{
		get { return m_endTime;}
		set {
			Reset ();
			endTime = value;
		}
	}

}

public enum TimerState {
	NONE,
	FINISHED,
	STARTED
};

/*******************************************************
					Asmos ©
Script fait pour Brütal Kartoffel

Role ........ : -Chronomètre
				-Minuteur
				-Montre à gousset
				-Machine à café
Participant . : Warmachine
Dernier dev . : Warmachine
Version ..... : V1.3

********************************************************/
