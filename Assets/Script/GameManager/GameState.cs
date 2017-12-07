using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour{

	public virtual void Begin (){
		this.enabled = true;
	}
	public virtual void End (){
		this.enabled = false;
	}

}
