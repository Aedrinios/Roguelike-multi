using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public string[] tabPowerPotions;

    // Use this for initialization
    void Start()
    {
        tabPowerPotions = new string[3];

        var monArray = new List<string>(3);
        monArray.Add("Stun");
        monArray.Add("Degats");
        monArray.Add("Heal");
        for(var i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, monArray.Count);
            tabPowerPotions[i] = monArray[rand];
            monArray.RemoveAt(rand);
        }    
    }
}
