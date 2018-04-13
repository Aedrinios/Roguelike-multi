using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharState : GameState
{

    public GameObject[] dummies;
    //public GameObject resetZone;
    public PlayerUI[] playerUI;
    private Dictionary<InputSet, GameObject> dummiesToInputsDictionary = new Dictionary<InputSet, GameObject>();
    private ControllerDelivery controllerDelivery;
    private List<GameObject> players = new List<GameObject>();


    void Update()
    {
        

        if (ValidationPlatform.validatedPlatforms == players.Count && players.Count > 1)
        {
            GameManager.instance.SetGameState(this.GetComponent<PlayState>());
        }
    }
    public override void Begin()
    {
        
        base.Begin();
        SetDummies();
        foreach (GameObject dummy in dummies)
        {
            if (dummy != null)
            {
                dummy.SetActive(true);
            }
        }
        SetupControllerDelivery();
        controllerDelivery.enabled = true;
    }
    void SetupControllerDelivery()
    {
        controllerDelivery = this.GetComponent<ControllerDelivery>();
        controllerDelivery.StartPressed = StartPressed;
        controllerDelivery.ReturnPressed = ReturnPressed;
    }

    public override void End()
    {
        base.End();
        foreach (GameObject dummy in dummies)
            dummy.SetActive(false);
        controllerDelivery.enabled = false;
        //resetZone.GetComponent<PlayerSelectionResetZone>().Deactivate();
        ValidationPlatform.DoorsOpened();
        GameManager.instance.players = players;
    }

    void StartPressed(InputSet inputSet)
    {
        

        if (dummiesToInputsDictionary.ContainsKey(inputSet))
            AddPlayer(inputSet);
        else if (dummiesToInputsDictionary.Count == dummies.Length)
            return;
        else
            AddSelectionDummy(inputSet);
    }

    private void AddSelectionDummy(InputSet inputSet)
    {
        PlayerSelectionDummy dummy = null;
        if (!dummiesToInputsDictionary.ContainsKey(inputSet))
        {
            dummy = GetUnusedDummy();
            dummiesToInputsDictionary.Add(inputSet, dummy.gameObject);
        }
        else
        dummy = dummiesToInputsDictionary[inputSet].GetComponent<PlayerSelectionDummy>();
        BindInputs(inputSet, dummy, inputSet.isController);
        dummy.gameObject.SetActive(true);
        dummy.Possess();
    }

    private PlayerSelectionDummy GetUnusedDummy()
    {
        for (int i = 0; i < dummies.Length; ++i)
            if (!dummiesToInputsDictionary.ContainsValue(dummies[i]))
                return dummies[i].GetComponent<PlayerSelectionDummy>();
        return null;    //Should not happen
    }

    private void BindInputs(InputSet inputSet, PlayerSelectionDummy dummy, bool isController)
    {
        object[] paramLeft = new object[2];
        paramLeft[0] = -1;
        paramLeft[1] = dummy;
        object[] paramRight = new object[2];
        paramRight[0] = 1;
        paramRight[1] = dummy;
        inputSet.AddInput("left-selection", SetDummySelection, isAxis: isController, parameters: paramLeft);
        inputSet.AddInput("right-selection", SetDummySelection, isAxis: isController, parameters: paramRight);
        inputSet.isActive = true;
    }

    void ReturnPressed(InputSet inputSet)
    {
        if (dummiesToInputsDictionary.ContainsKey(inputSet) && dummiesToInputsDictionary[inputSet].activeSelf)
        {
            dummiesToInputsDictionary[inputSet].GetComponent<PlayerSelectionDummy>().Reset();
            inputSet.Clear();
            inputSet.isActive = false;
            dummiesToInputsDictionary.Remove(inputSet);
        }
    }

    void AddPlayer(InputSet inputSet)
    {
        bool exists=false;
        foreach(GameObject p in players)
        {
            if(p.GetComponent<CharController>().GetInputs()== inputSet)
            {
                exists = true;
            }
        }
        if(!exists)
        {
            PlayerSelectionDummy dummy = dummiesToInputsDictionary[inputSet].GetComponent<PlayerSelectionDummy>();
            GameObject selectedPlayer = dummy.GetSelectedPlayer();
            GameObject createdPlayer = Instantiate(selectedPlayer, dummy.transform.position, Quaternion.identity);
            players.Add(createdPlayer);
            inputSet.Clear();
            createdPlayer.GetComponent<CharController>().SetInputs(inputSet);
            dummiesToInputsDictionary[inputSet].SetActive(false);
            createdPlayer.GetComponent<Character>().SetUI(GetUnusedPlayerUI());
        }
    }

    private PlayerUI GetUnusedPlayerUI()
    {
        for (int i = 0; i < playerUI.Length; ++i)
            if (playerUI[i].IsUnused())
                return playerUI[i];       
        return null;    //Should not happen
    }

    void SetDummySelection(params object[] args)
    {
        PlayerSelectionDummy dummy = (PlayerSelectionDummy)args[1];
        dummy.SelectedPlayer += (int)args[0];
    }

    public void RemovePlayer(GameObject player)
    {
        InputSet inputSet = player.GetComponent<CharController>().GetInputs();
        player.GetComponent<Character>().UI.RemovePlayer();
        inputSet.Clear();
        players.Remove(player);
        Destroy(player);
        AddSelectionDummy(inputSet);
    }

    public void SetDummies()
    {
        var a = GameObject.Find("Player Selection dummy");
        var b = GameObject.Find("Player Selection dummy (1)");
        var c = GameObject.Find("Player Selection dummy (2)");
        var d = GameObject.Find("Player Selection dummy (3)");
        dummies[0] = a;
        dummies[1] = b;
        dummies[2] = c;
        dummies[3] = d;
    }
}
