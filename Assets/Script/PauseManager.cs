using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    // Menu Pause 
    public GameObject panelPause;
    public GameObject pause;
    public GameObject quitePause;
    public GameObject replayPause;

    public Sprite notSelectedQuitePause;
    public Sprite selectedQuitePause;
    public Sprite notSelectedReplayPause;
    public Sprite selectedReplayPause;

    private int numb;
    private bool isActif;

    // TEST JUSTINE
    public GameObject credit;
    // FIN TEST

    // Use this for initialization
    void Start () {
        isActif = false; 
        panelPause.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
        quitePause.gameObject.SetActive(false);
        replayPause.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        var butSelect1Manette = UnityEngine.Input.GetButtonDown("Joystick 1 select"); // Bouton select manette
        var butSelect2Manette = UnityEngine.Input.GetButtonDown("Joystick 2 select"); // Bouton select manette
        var butSelect3Manette = UnityEngine.Input.GetButtonDown("Joystick 3 select"); // Bouton select manette
        var butSelectManette = UnityEngine.Input.GetButtonDown("Joystick 4 select"); // Bouton select manette

        var butStart1Manette = UnityEngine.Input.GetButtonDown("Joystick 1 start"); // Bouton start manette
        var butStart2Manette = UnityEngine.Input.GetButtonDown("Joystick 2 start"); // Bouton start manette
        var butStart3Manette = UnityEngine.Input.GetButtonDown("Joystick 3 start"); // Bouton start manette
        var butStart4Manette = UnityEngine.Input.GetButtonDown("Joystick 4 start"); // Bouton start manette

        var butSelectClavier1 = UnityEngine.Input.GetButtonDown("Keyboard 1 select"); // Space bar pour selectionner
        var butSelectClavier2 = UnityEngine.Input.GetButtonDown("Keyboard 2 select"); // Space bar pour selectionner

        var butStartClavier1 = UnityEngine.Input.GetButtonDown("Keyboard 1 start"); // Space bar pour selectionner
        var butStartClavier2 = UnityEngine.Input.GetButtonDown("Keyboard 2 start"); // Space bar pour selectionner

        if (butSelect1Manette || butSelect2Manette || butSelect3Manette || butSelectManette || butSelectClavier1 || butSelectClavier2)
        {
            menuPause();
        }

        if (isActif==true)
        {
            if (Input.GetKeyDown(KeyCode.Z)) // haut
            {
                numb = 0;
                changeSprite(numb);
            }
            if (Input.GetKeyDown(KeyCode.S)) // bas
            {
                numb = 1;
                changeSprite(numb);
            }

            if (numb == 0 && (butStart1Manette || butStart2Manette || butStart3Manette || butStart4Manette || butStartClavier1 || butStartClavier2)) // Selection de play
            {
                panelPause.gameObject.SetActive(false);
                pause.gameObject.SetActive(false);
                quitePause.gameObject.SetActive(false);
                replayPause.gameObject.SetActive(false);
                Time.timeScale = 1.0f;

            }
            if (numb == 1 && (butStart1Manette || butStart2Manette || butStart3Manette || butStart4Manette || butStartClavier1 || butStartClavier2)) // Selection de quite
            {
                SceneManager.LoadScene("Menu");
                Time.timeScale = 1.0f;
            }
        }

    }

    public void menuPause()
    {
        isActif = true; 
        Time.timeScale = 0; 
        panelPause.gameObject.SetActive(true);
        pause.gameObject.SetActive(true);
        quitePause.gameObject.SetActive(true);
        replayPause.gameObject.SetActive(true);
    }

    public void changeSprite(int i)
    {
        if (i == 0)
        {
            quitePause.GetComponent<UnityEngine.UI.Image>().overrideSprite = notSelectedQuitePause;
            replayPause.GetComponent<UnityEngine.UI.Image>().overrideSprite = selectedReplayPause;
        }

        if (i == 1)
        {
            replayPause.GetComponent<UnityEngine.UI.Image>().overrideSprite = notSelectedReplayPause;
            quitePause.GetComponent<UnityEngine.UI.Image>().overrideSprite = selectedQuitePause;
        }
    }
}
