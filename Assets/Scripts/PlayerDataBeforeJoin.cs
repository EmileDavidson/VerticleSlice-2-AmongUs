using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerDataBeforeJoin : NetworkBehaviour
{
    public string playername = "PlayerName";
    public Color playerColor = Color.white;

    public string Playername
    {
        get => playername;
        set => playername = value;
    }
}
