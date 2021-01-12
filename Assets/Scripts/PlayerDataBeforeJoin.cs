using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBeforeJoin : MonoBehaviour
{
    public string playername = "PlayerName";
    public Color playerColor = Color.white;
    public string outfit = "none";
    public string hat = "none";
    public string pet = "none";

    public string Playername
    {
        get => playername;
        set => playername = value;
    }
}
