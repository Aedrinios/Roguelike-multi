using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour {

    // Position A -> Insert Coin (default)
    // Position B -> Quite
    private GameObject insert;
    private GameObject quite;
    private GameObject credit;
    public GameObject panelCredit;
    public Sprite notSelectedInsert;
    public Sprite selectedInsert;
    public Sprite notSelectedQuite;
    public Sprite selectedQuite;
    public Sprite notSelectedCredit;
    public Sprite selectedCredit;
    public bool isCredit;

    private int numb; // 0 -> play, 1 -> quite
    private int numbBis; // 0 -> play, 1 -> quite

    public GameObject retour;

    // Use this for initialization
    void Start () {
        insert = GameObject.Find("Insert");
        quite = GameObject.Find("Quite");
        credit = GameObject.Find("Credit");
        panelCredit.gameObject.SetActive(false);
        numb = 0;
    }
	
	// Update is called once per frame
	void Update () {

        var keyboardY = UnityEngine.Input.GetAxis("Keyboard 2 Y axis"); // KeyBord haut/bas 
        var keyboardX = UnityEngine.Input.GetAxis("Keyboard 2 X axis"); // KeyBord gauche/droit 
        var butSelect = UnityEngine.Input.GetButtonDown("Keyboard 2 start"); // Bouton W pour selectionner

        var joystick= UnityEngine.Input.GetAxis("Joystick 1 X axis"); // Joystick gauche/droite 
        var joystick2 = UnityEngine.Input.GetAxis("Joystick 1 Y axis"); // Joystick haut/bas 
        var butStart = UnityEngine.Input.GetButtonDown("Joystick 1 start"); // Bouton start manette
        var butA = UnityEngine.Input.GetButtonDown("Joystick 1 primary"); // Bouton A manette

        /*
         * Faire pour tout les clavier/manettes
         */
        var butHaut = UnityEngine.Input.GetKeyDown(KeyCode.Z); // Bouton haut 
        var butBas = UnityEngine.Input.GetKeyDown(KeyCode.S); // Bouton Bas
        
        switch (isCredit)
        {
            case (true):
                Debug.Log("dans is credit");
                Debug.Log(numb);

                if ((butSelect || butStart || butA)) // quitte les credits
                {
                    panelCredit.gameObject.SetActive(false);
                    isCredit=false;
                }
                break;
            case (false):
                if (butHaut /*keyboardY > 0 || keyboardX < 0 || joystick < 0 || joystick2 > 0*/) // haut
                {
                    // Changer sprite
                    changeSprite(quite, numb);
                    if (numb > 0)
                    {
                        numb--;
                    }
                }
                if (butBas /*keyboardY < 0 || keyboardX > 0 || joystick > 0 || joystick2 < 0*/) // bas
                {
                    // Changer Sprite
                    changeSprite(quite, numb);
                    if (numb < 2)
                    {
                        numb++;
                    }
                }
                if (numb == 0 && (butSelect || butStart || butA)) // Selection de play
                {
                    // Changer de scene
                    SceneManager.LoadScene("TestGeneration");

                }
                if (numb == 1 && (butSelect || butStart || butA)) // Selection de quite
                {
                    Application.Quit();
                }
                if (numb == 2 && (butSelect || butStart || butA)) // Selection de quite
                {
                    panelCredit.gameObject.SetActive(true);
                    isCredit=true;
                }
                break;
        }
    }

    public void changeSprite(GameObject objt, int i)
    {
        if (i == 0) // Play
        {
            quite.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedQuite;
            insert.gameObject.GetComponent<SpriteRenderer>().sprite = selectedInsert;
            credit.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedCredit;
        }

        if(i==1) // Quit
        {
            quite.gameObject.GetComponent<SpriteRenderer>().sprite = selectedQuite;
            insert.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedInsert;
            credit.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedCredit;
        }

        if (i == 2) // Credit
        {
            credit.gameObject.GetComponent<SpriteRenderer>().sprite = selectedCredit;
            insert.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedInsert;
            quite.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedQuite;
        }
    }
}
