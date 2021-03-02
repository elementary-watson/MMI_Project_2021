using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_Reference : MonoBehaviour
{
    private IDictionary<int, string> allplayers = new Dictionary<int, string>();
    // Start is called before the first frame update
    
    public void setPlayers(IDictionary<int, string> allplayers)
    {
        this.allplayers = allplayers;
    }
    public IDictionary<int, string> getPlayers()
    {
        return allplayers;
    }
    public void addPlayer(int id, string charname)
    {
        if(!allplayers.Keys.Contains(id))
        allplayers.Add(id, charname);
    }
    public void deleteplayer(int photonId)
    {
        if (allplayers.ContainsKey(photonId))
        { // check key before removing it
            allplayers.Remove(photonId);
        }
    }
    public void readPlayer()
    {
        foreach (KeyValuePair<int, string> kvp in allplayers) { 
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            print("Key: " + kvp.Key + "Value" + kvp.Value);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
