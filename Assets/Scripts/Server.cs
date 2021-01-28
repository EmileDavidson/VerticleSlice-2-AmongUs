using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Server : NetworkBehaviour
{
    [Header("PlayerData (has to be synced with players)")]
    [SyncVar(hook = nameof(SpeedChanged))][SerializeField] public float speed = 5;
    [SyncVar][SerializeField] private List<Color> colorList;
    // [SyncVar][SerializeField] private List<Color> usableColorList;
    [SyncVar][SerializeField] public int playersInGame;

    public void SpeedChanged(float oldVal, float newVal)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerData>().speed = newVal;
        }
    }

    [Server]
    public void ColorsChanged(List<Color> newVal)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<ServerToClientCommunicator>().ColorListUpdate(colorList);
        }
    }


    [Server]
    public void ColorListRemoveColor(Color color)
    {
        colorList.Remove(color);
        ColorsChanged(colorList);
    }
    
    [Server]
    public void ColorListAddColor(Color col)
    {
        colorList.Add(col);
        ColorsChanged(colorList);
    }

    [Server]
    public List<Color> GetUsableColors()
    {
        return colorList;
    }
}
