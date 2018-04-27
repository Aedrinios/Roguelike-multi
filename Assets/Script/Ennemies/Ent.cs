using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent : AnimateEntity {

	public Shrub[] shrubs;
	public GameObject[] spells;
	public float timeBewteenSpells;
	public GameObject portal;

	private GameObject currentTarget;
	private List<GameObject> players;
	private int startHealth;
	private float opacity = 255;
	private bool done = false;
	private BoxCollider2D boxCollider;
    

    //variables d'états
    private bool isPhase1;
	private bool isPhase2;
	private bool isPhase3;
	private bool deadSound = false;

	//utilitaires
	private bool doOnce = false;
	private float timeCounterSpells = 0;
	private float timeCounter = 0;

    private void Awake()
    {
        base.Awake();
        startHealth = health;
    }

    // Use this for initialization
    void Start () {
        musiqueManager.stopSound("GloomyForestWav");
        musiqueManager.playSound("bossTheme");
        base.Start();
		scaleMultiplier = 6f;
		timeCounterSpells = timeBewteenSpells;
		boxCollider = GetComponent<BoxCollider2D>();

		setActivePhase(1);
	}

	// Update is called once per frame
	void Update () {

		//Cheat Code
		if (Input.GetKeyDown("b"))
		{
			this.GetComponent<AnimateEntity>().ReceiveHit(500,gameObject);
		}

		if (Input.GetKeyDown("n"))
		{
			this.GetComponent<AnimateEntity>().ReceiveHit(20,gameObject);
		}	

		//VARIABLE DE COMPTAGE DE TEMPS
		timeCounterSpells += Time.deltaTime;


		//1ère phase : Au dessus de 50%
		if (isPhase1) 
		{
			castRandomSpellOnRandomTarget(1);          //fait une attaque au harsard sur la cible choisi au hasard

			//CHANGEMENT DE PHASE
			if (health<=startHealth/2 && getNumberOfShrubsAlive() > 0)
			{
				setActivePhase(2);
			}
			else if (health <= 0)
			{
				setActivePhase(3);
			}
		}
		//2ème phase : En dessous de 50%
		else if (isPhase2)
		{
			castRandomSpellOnRandomTarget(2);            //fait une attaque au harsard sur la cible choisi au hasard
			claimHealing();            //Les abrisseaux le soignent

			//CHANGEMENT DE PHASE
			if (health == 350)
			{
				setActivePhase(1);
				shrubStopPlaySound();
			}
			else if (getNumberOfShrubsAlive() == 0)
			{
				setActivePhase(3);
			}
		}
		//3ème phase : Les shrubs sont morts
		else if (isPhase3)
		{
			castRandomSpellOnRandomTarget(3);

			if (health<=0)
			{
				boxCollider.enabled = false;
                
				animator.SetBool("isDead", true);
				if (!deadSound) {
					SoundManager.playSound ("bossDeath");
					deadSound = true;
				}
				timeCounter += Time.deltaTime;
				if (timeCounter >= 2)
				{
					if (!done)
					{
						Instantiate(portal, transform.position, Quaternion.identity);
                        musiqueManager.stopSound("bossTheme");
                        SoundManager.playSound("musique_victoire");
                        done = true;
                        this.enabled = false;
					}
				}

			}
		}
	}


	//Cette fonction choisi une nouvelle cible parmis tous les joueurs instanciés de façon aléatoire et la met dans currentTarget.
	void chooseRandomTarget()
	{
		int rdm = Mathf.RoundToInt(Random.Range(0, GameManager.instance.players.Count));
		currentTarget = GameManager.instance.players[rdm];
	}

	void castRandomSpellOnRandomTarget(int n)
	{
		GameObject previousTarget=null;
		GameObject spell = null;
		Vector3 pos = new Vector3(0, 0, 0);

		if (timeCounterSpells >= timeBewteenSpells)
		{
			for (int i = 0; i < n; i++)
			{
				do          //Ce Do while sert à choisir deux cible différentes lorsque le boss cast deux spells
				{
					chooseRandomTarget();
				} while (currentTarget == previousTarget);
				previousTarget = currentTarget;

				int rdm = Mathf.RoundToInt(Random.Range(0, spells.Length));

				spell = spells[rdm];
				switch (spell.name)
				{
				case "SmashAttack":
					pos = currentTarget.transform.position + new Vector3(1, 0, 0);
                    Instantiate(spell, pos, Quaternion.identity);
                    break;
				case "SwingAttack":
					pos = currentTarget.transform.position + new Vector3(2, 0, 0); ;
                    Instantiate(spell, pos, Quaternion.identity);
                    break;
                case "throwSeed":
                    pos = transform.position + new Vector3(3, -3, 0);
                    GameObject attack = Instantiate(spell, pos, Quaternion.identity);
                    attack.gameObject.GetComponent<AttackThrow>().setTarget(currentTarget);
                    break;
				}
                

				
			}

			timeCounterSpells = 0;
		}
	}

	void setAllShrubsCanBeDamaged(bool b)
	{
		for (int i = 0; i < shrubs.Length; i++)
		{
			shrubs[i].setCanBeDamaged(b);
		}
	}

	void claimHealing()
	{
		for (int i = 0; i < shrubs.Length; i++)
		{
			shrubs[i].healEnt();
		}
	}

	void stopHealing()
	{
		for (int i = 0; i < shrubs.Length; i++)
		{
			shrubs[i].stopHealing();
		}
	}

	int getNumberOfShrubsAlive()
	{
		int res = 0;

		for (int i = 0; i < shrubs.Length; i++)
		{
			if (!shrubs[i].getIsDead())
			{
				res++;
			}
		}
		return res;
	}

	void setActivePhase(int i)
	{
		switch (i)
		{
		case 1:
			isPhase1 = true;
			isPhase2 = false;
			isPhase3 = false;
			animator.SetBool("isphase2", false);

			setCanBeDamaged(true);                //vulnérable
			setAllShrubsCanBeDamaged(false);      //arbrisseaux invulnérables
			stopHealing();
			break;
		case 2:
			isPhase1 = false;
			isPhase2 = true;
			isPhase3 = false;
			animator.SetBool("isphase2", true);

			setAllShrubsCanBeDamaged(true);        //Les arbrisseaux son vulnérable
			setCanBeDamaged(false);                //Invulnérable
			break;
		case 3:
			isPhase1 = false;
			isPhase2 = false;
			isPhase3 = true;

			animator.SetBool("isphase3", true);

			setCanBeDamaged(true);                //Invulnérable
			break;
		}
	}

	int getActivePhase()
	{
		if (isPhase1)
		{
			return 1;
		}
		else if (isPhase2)
		{
			return 2;
		}
		else
		{
			return 3;
		}
	}

    protected override void KnockBack(GameObject other)
    {
        //Elle ne fait rien pour désactiver le knockback sur le boss
    }

    void spawnShrubs(bool b)
	{
		for (int i = 0; i < shrubs.Length; i++)
		{
			shrubs[i].gameObject.SetActive(b);
		}
	}

	public int getStartHealth()
	{
		return startHealth;
	}

	void shrubStopPlaySound()
	{
		for (int i = 0; i < shrubs.Length; i++)
		{
			shrubs[i].stopPlaySound();
		}
	}
}
