using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : InanimateEntity {
	public effects effect;
	public float slowEffect;
	public float effectTime;
	public override void Use (Character user) {
		switch (effect){
			case effects.SLOW:
				Debug.Log("Player slowed");
				break;
			//Ici, un switch bien moche des familles sur les effets
			default :
				Debug.Log("nothing here yet");
				break;
		}
	}
}

public enum effects {
	SLOW,
	STUN,
	DOT,
	DISORIENTATION,
	MOONWALK,
	WEAK,
	FRENZY,
	AGGRO
}
