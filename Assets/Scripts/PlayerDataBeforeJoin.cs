using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerDataBeforeJoin : NetworkBehaviour
{
    public string playername = "PlayerName";
    [SyncVar] public Color playerColor = Color.white;
    public string outfit = "none";
    public string hat = "none";
    public string pet = "none";

    public string Playername
    {
        get => playername;
        set => playername = value;
    }
}
