using System;
using System.Collections.Generic;
using Mirror;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    public override void OnStartLocalPlayer()
    {
        FixPlayerName();
        FixPlayerColor();
    }

    private void FixPlayerName()
    {
        string playername = GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playername;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (GameObject obj in gameObjects)
        {
            Player player = obj.GetComponent<Player>();
            if (!player._playerData.playerName.StartsWith(playername)) continue;
            i++;
        }
        if (i > 0) playername = playername + i;
        CmdSetupPlayerName(playername);
    }
    private void FixPlayerColor()
    {
        Color color = GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playerColor;
        CmdSetupPlayerColor(color);
    }

    [Command]
    public void CmdSetupPlayerName(string newPlayerName)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        _playerData.playerName = newPlayerName;
    }
    
    [Command]
    public void CmdSetupPlayerColor(Color color)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        int num = Random.Range(0, server.GetComponent<Server>().GetUsableColors().Count - 1);
        if (color == Color.white || !server.GetComponent<Server>().GetUsableColors().Contains(color))
        {
            color = new Color(server.GetComponent<Server>().GetUsableColors()[num].r, server.GetComponent<Server>().GetUsableColors()[num].g, server.GetComponent<Server>().GetUsableColors()[num].b, 1);
            server.GetComponent<Server>().ColorListRemoveColor(color);
        }
        _playerData.playerColor = color;
        RpcChangeDataColor(color); //return the color back to the player so he can remember it even when leaving the game. an joining a new game
    }

    [TargetRpc]
    public void RpcChangeDataColor(Color color)
    {
        GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playerColor = color;
    }
}
