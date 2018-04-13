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
    public bool endOfGame;

    private int numb; // 0 -> play, 1 -> quite
    private int numbBis; // 0 -> play, 1 -> quite

    public GameObject retour;

    private bool bouge;

    // Use this for initialization
    void Start () {
        insert = GameObject.Find("Insert");
        quite = GameObject.Find("Quite");
        credit = GameObject.Find("Credit");
        panelCredit.gameObject.SetActive(endOfGame);
        numb = 0;
        bouge = true;
    }
	
	// Update is called once per frame
	void Update () {

        // Bouton start
        var butKeyboardStart1 = UnityEngine.Input.GetButtonDown("Keyboard 1 start"); // Bouton K pour selectionner
        var butKeyboardStart2 = UnityEngine.Input.GetButtonDown("Keyboard 2 start"); // Bouton W pour selectionner
        var butJoystickStart1 = UnityEngine.Input.GetButtonDown("Joystick 1 start"); // Bouton start manette
        var butJoystickStart2 = UnityEngine.Input.GetButtonDown("Joystick 2 start"); // Bouton start manette
        var butJoystickStart3 = UnityEngine.Input.GetButtonDown("Joystick 3 start"); // Bouton start manette
        var butJoystickStart4 = UnityEngine.Input.GetButtonDown("Joystick 4 start"); // Bouton start manette

        // Axe de déplacement
        var keyboardAxisY1 = UnityEngine.Input.GetAxis("Keyboard 1 Y axis"); // KeyBord haut/bas 
        var keyboardAxisY2 = UnityEngine.Input.GetAxis("Keyboard 2 Y axis"); // KeyBord haut/bas 
        var JoystickAxisY1 = UnityEngine.Input.GetAxis("Joystick 1 Y axis"); // KeyBord haut/bas 
        var JoystickAxisY2 = UnityEngine.Input.GetAxis("Joystick 2 Y axis"); // KeyBord haut/bas 
        var JoystickAxisY3 = UnityEngine.Input.GetAxis("Joystick 3 Y axis"); // KeyBord haut/bas 
        var JoystickAxisY4 = UnityEngine.Input.GetAxis("Joystick 4 Y axis"); // KeyBord haut/bas 

        switch (isCredit)
        {
            case (true):
                Debug.Log("dans is credit");
                Debug.Log(numb);

                // Quitte les crédit à la fin du jeu (ramene au menu)
                if (endOfGame && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4))
                {
                    SceneManager.LoadScene("Menu");
                }

                // quitte les credits dans le menu 
                if ((butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) 
                {
                    panelCredit.gameObject.SetActive(false);
                    isCredit=false;
                }
                break;
            case (false):
                // Haut
                if(bouge && (keyboardAxisY1 > 0 || keyboardAxisY2 > 0 || JoystickAxisY1 > 0 || JoystickAxisY2 > 0 || JoystickAxisY3 > 0 || JoystickAxisY4 > 0))
                {
                    // Changer sprite
                    changeSprite(quite, numb);
                    if (numb > 0)
                    {
                        numb--;
                    }
                    bouge = false;
                    StartCoroutine("timer");
                }
                // Bas
                if (bouge && (keyboardAxisY1 < 0 || keyboardAxisY2 < 0 || JoystickAxisY1 < 0 || JoystickAxisY2 < 0 || JoystickAxisY3 < 0 || JoystickAxisY4 < 0)) // bas
                {
                    // Changer Sprite
                    changeSprite(quite, numb);
                    if (numb < 2)
                    {
                        numb++;
                    }
                    bouge = false;
                    StartCoroutine("timer");
                }
                if (numb == 0 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) // Selection de play
                {
                    // Changer de scene
                    SceneManager.LoadScene("TestGeneration");

                }
                if (numb == 1 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) // Selection de quite
                {
                    Application.Quit();
                }
                if (numb == 2 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) // Selection de quite
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

    public IEnumerator timer()
    {
        yield return new WaitForSeconds(0.1f);
        bouge = true;
    }
}
