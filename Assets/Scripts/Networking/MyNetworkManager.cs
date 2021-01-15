using System.Collections;
using System.Collections.Generic;
using Mirror;
using Telepathy;
using UnityEngine;
using UnityEngine.UIElements;

public class MyNetworkManager : NetworkManager
{
    [SerializeField] private GameObject serverObj;
    public override void OnStartServer()
    {
        base.OnStartServer();
        GameObject server = Instantiate(serverObj);
        server.name = "Server";
        print("<COLOR=GREEN>Server Started</COLOR>");
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        print("<COLOR=RED>Server Stopped</COLOR>");
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        print("<COLOR=GREEN>Connected to server</COLOR>");
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        print("<COLOR=RED>Disconnected from server</COLOR>");
    }
    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        print("Create Player");
         GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocations");
         int num = GameObject.Find("Server").GetComponent<Server>().playersInGame;
         Transform startPos = SpawnLocations[num].transform;
         
         print(startPos);
         GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
         
         GameObject.Find("Server").GetComponent<Server>().playersInGame++;
         NetworkServer.AddPlayerForConnection(conn, player);
    }
}
