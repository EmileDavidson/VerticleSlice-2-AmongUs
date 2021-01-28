using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using UnityEngine;
using Mirror;

public class PlayerData : NetworkBehaviour
{

    public TextMesh playerNameText;
    public Material playerMaterialClone;
    
    [SyncVar] public float speed;
    [SyncVar(hook = nameof(OnNameChanged))] public string playerName;
    [SyncVar(hook = nameof(OnColorChanged))] public Color playerColor = Color.white;
    [SyncVar] public bool canMove = true;
    [SyncVar] [SerializeField] public List<Color> colors = new List<Color>();
    [SerializeField] private GameObject playerBody;
    
    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = _New;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerMaterialClone = new Material(playerBody.GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        playerBody.GetComponent<Renderer>().material = playerMaterialClone;
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
        speed = server.GetComponent<Server>().speed;
    }
}
