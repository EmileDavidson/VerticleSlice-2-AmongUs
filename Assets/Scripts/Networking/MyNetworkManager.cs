using System.Collections;
using System.Collections.Generic;
using Mirror;
using Telepathy;
using UnityEngine;
using UnityEngine.UIElements;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        print("Server Started");
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
        print("Server stopped");
    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        print("Connected to server");
    }
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        print("Disconnected from server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        print("Create Player");
         // startPos = GetStartPosition();
         GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocations");
         Transform startPos = SpawnLocations[NetworkServer.connections.Count].transform;
         print(startPos);
         GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
