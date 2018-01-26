using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour {

    // Position A -> Insert Coin (default)
    // Position B -> Quite
    private GameObject insert;
    private GameObject quite;
    private GameObject insertSelect;
    public Sprite notSelectedQuite;
    public Sprite selectedQuite;

    private int numb; // 0 -> play, 1 -> quite

	// Use this for initialization
	void Start () {
        insert = GameObject.Find("Insert");
        quite = GameObject.Find("Quite");
        insertSelect = GameObject.Find("SelectedInsert");
        numb = 0;
    }
	
	// Update is called once per frame
	void Update () {

        var butT = UnityEngine.Input.GetButtonDown("Keyboard 2 left-selection"); // Bouton Q pour gauche
        var butG = UnityEngine.Input.GetButtonDown("Keyboard 2 right-selection"); // Bouton D pour droite
        var butB = UnityEngine.Input.GetButtonDown("Keyboard 2 start"); // Bouton W pour selectionner

        if (butT) // gauche
        {
            // Changer sprite
            changeSprite(quite, 0);
            numb = 0;
        }
        if (butG) // droite
        {
            // Changer Sprite
            changeSprite(quite, 1);
            numb = 1;
        }

        if(numb == 0 && butB) // Selection de play
        {
            // Changer de scene
            SceneManager.LoadScene("BastienTest");

        }
        if (numb == 1 && butB) // Selection de quite
        {
            Application.Quit();
        }
    }

    public void changeSprite(GameObject objt, int i)
    {
        if (i == 0)
        {
            objt.gameObject.GetComponent<SpriteRenderer>().sprite = notSelectedQuite;
            // Hide NotSelectedInsert 
            insert.gameObject.SetActive(false);
            // Afficher object selectionné 
            insertSelect.gameObject.SetActive(true);
        }

        if(i==1)
        {
            objt.gameObject.GetComponent<SpriteRenderer>().sprite = selectedQuite;
            // Hide selectedInsert
            insertSelect.gameObject.SetActive(false);
            // Afficher object non selectionné
            insert.gameObject.SetActive(true);
        }
    }
}
