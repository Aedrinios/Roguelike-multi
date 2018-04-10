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
    public Sprite notSelectedInsert;
    public Sprite selectedInsert;
    public Sprite notSelectedQuite;
    public Sprite selectedQuite;
    public Sprite notSelectedCredit;
    public Sprite selectedCredit;

    private int numb; // 0 -> play, 1 -> quite

	// Use this for initialization
	void Start () {
        insert = GameObject.Find("Insert");
        quite = GameObject.Find("Quite");
        credit = GameObject.Find("Credit");
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


        if (keyboardY > 0 || keyboardX < 0 || joystick<0 || joystick2>0) // gauche / haut
        {
            // Changer sprite
            changeSprite(quite, numb);
            if (numb > 0)
            {
                numb--;
            }
        }
        if (keyboardY < 0 || keyboardX > 0 || joystick>0 || joystick2<0) // droite / bas
        {
            // Changer Sprite
            changeSprite(quite, numb);
            if (numb <2)
            {
                numb++;
            }
        }

        Debug.Log(numb);

        if(numb == 0 && (butSelect || butStart || butA)) // Selection de play
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
            SceneManager.LoadScene("Credit");
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
