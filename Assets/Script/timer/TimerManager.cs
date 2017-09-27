using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {
	private static ArrayList timers = new ArrayList (); 

	void Update () {
		foreach (Timer t in timers)
			t.m_time += Time.deltaTime;
	}
		
	public static void SetupTimer(Timer t){
		timers.Add (t);
	}
}

/*******************************************************
					Asmos ©
Script fait pour Brütal Kartoffel

Role ........ : -Gère les timers du jeu
				-Donne des timers au monde

Participant . : Warmachine
Dernier dev . : Warmachine
Version ..... : V1.0
********************************************************/