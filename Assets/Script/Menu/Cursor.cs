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
        if (endOfGame)
        {
            panelCredit.gameObject.SetActive(true);
        }
        numb = 0;
        bouge = true;
       // isCredit = false;
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
        var keyboardAxisY1 = UnityEngine.Input.GetAxis("Keyboard 1 Y axis"); // KeyBord1 haut/bas 
        var keyboardAxisY2 = UnityEngine.Input.GetAxis("Keyboard 2 Y axis"); // KeyBord2 haut/bas 
        var JoystickAxisY1 = UnityEngine.Input.GetAxis("Joystick 1 Y axis"); // Joystick1 haut/bas 
        var JoystickAxisY2 = UnityEngine.Input.GetAxis("Joystick 2 Y axis"); // Joystick2 haut/bas 
        var JoystickAxisY3 = UnityEngine.Input.GetAxis("Joystick 3 Y axis"); // Joystick3 haut/bas 
        var JoystickAxisY4 = UnityEngine.Input.GetAxis("Joystick 4 Y axis"); // Joystick4 haut/bas 
        
        switch (isCredit)
        {
            case (true):
                // Quitte les crédit à la fin du jeu (ramene au menu)
                if (endOfGame && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4))
                {
                    SceneManager.LoadScene("Menu");
                }

                // quitte les credits dans le menu 
                if (!endOfGame && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) 
                {
                    panelCredit.gameObject.SetActive(false);
                    isCredit =false;
                }
                break;
            case (false):
                // Sur Insert Coin
                if (numb == 0) 
                {
                    // Bas
                    if (bouge && (keyboardAxisY1 < 0 || keyboardAxisY2 < 0 || JoystickAxisY1 < 0 || JoystickAxisY2 < 0 || JoystickAxisY3 < 0 || JoystickAxisY4 < 0)) // bas
                    {
                        numb = 1;
                        // Changer Sprite
                        changeSprite(numb);
                        bouge = false;
                        StartCoroutine("timer");
                    }
                }
                // Sur Quit
                if (numb == 1) 
                {
                    // Haut
                    if (bouge && (keyboardAxisY1 > 0 || keyboardAxisY2 > 0 || JoystickAxisY1 > 0 || JoystickAxisY2 > 0 || JoystickAxisY3 > 0 || JoystickAxisY4 > 0))
                    {
                        numb = 0;
                        // Changer sprite
                        changeSprite(numb);
                        bouge = false;
                        StartCoroutine("timer");
                    }
                    // Bas
                    if (bouge && (keyboardAxisY1 < 0 || keyboardAxisY2 < 0 || JoystickAxisY1 < 0 || JoystickAxisY2 < 0 || JoystickAxisY3 < 0 || JoystickAxisY4 < 0))
                    {
                        numb = 2;
                        // Changer Sprite
                        changeSprite(numb);
                        bouge = false;
                        StartCoroutine("timer");
                    }
                }
                // Sur Credits
                if (numb == 2) 
                {
                    // Haut
                    if (bouge && (keyboardAxisY1 > 0 || keyboardAxisY2 > 0 || JoystickAxisY1 > 0 || JoystickAxisY2 > 0 || JoystickAxisY3 > 0 || JoystickAxisY4 > 0))
                    {
                        numb = 1;
                        // Changer sprite
                        changeSprite(numb);
                        bouge = false;
                        StartCoroutine("timer");
                    }
                }
                // Selection de play
                if (numb == 0 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) 
                {
                    // Changer de scene
                    SceneManager.LoadScene("TestGeneration");

                }
                // Selection de quite
                if (numb == 1 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4)) 
                {
                    Application.Quit();
                }
                // Selection de credit
                if (numb == 2 && (butKeyboardStart1 || butKeyboardStart2 || butJoystickStart1 || butJoystickStart2 || butJoystickStart3 || butJoystickStart4))
                {
                    panelCredit.SetActive(true);
                    isCredit=true; ;
                }
                break;
        }
    }

    public void changeSprite(int i)
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
        yield return new WaitForSeconds(0.5f);
        bouge = true;
    }
}
