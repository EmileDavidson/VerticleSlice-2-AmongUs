using System;
using System.Collections.Generic;
using Mirror;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    private PlayerData _playerData;
    public ServerToClientCommunicator serverToClientCommunicator;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        serverToClientCommunicator = GetComponent<ServerToClientCommunicator>();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), player.GetComponent<BoxCollider2D>(), true);
        }
    }


    public override void OnStartLocalPlayer()
    {
        FixPlayerName();
        FixPlayerColor();
        //we add this script here so its on the client only.. not on the server (because not everyone has to see this)
        this.gameObject.AddComponent<PlayerHandler>();
        gameObject.name = "Me";
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
    }
    
    public void UpdateColor(List<Color> clist)
    {
        serverToClientCommunicator.GetComponent<ServerToClientCommunicator>().ColorListUpdate(clist);
    }
}
