using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerData : NetworkBehaviour
{

    public TextMesh playerNameText;
    public Material playerMaterialClone;
    
    [SyncVar] public float speed;
    [SyncVar] public List<Color> colors = new List<Color>();
    
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = _New;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        GetComponent<Renderer>().material = playerMaterialClone;
    }
    
    public override void OnStartLocalPlayer()
    {
        CmdUpdateAllInfo();
    }

    [Command]
    public void CmdUpdateAllInfo()
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        colors = server.GetComponent<Server>().GetColors();
        speed = server.GetComponent<Server>().Speed;
    }

}
