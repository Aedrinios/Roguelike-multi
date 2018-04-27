using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerTester : MonoBehaviour {
	public Text m_text;
	Timer timerTest;

	void Start () {
		timerTest = new Timer (2.0f, methodeDeTest);
		timerTest.Play ();
	}

	void Update() {
		m_text.text = timerTest.m_time.ToString();
	}

	void methodeDeTest(){
	}
}

/*******************************************************
					Asmos ©
Script fait pour Brütal Kartoffel

Role ........ : -Test les classes Timer, TimerManager
				-Test évènements avec timers
				-Donne un exemple de timer
Participant . : Warmachine
Dernier dev . : Warmachine
Version ..... : V1.0
********************************************************/