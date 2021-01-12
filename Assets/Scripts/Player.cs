using System;
using System.Collections.Generic;
using Mirror;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    [Header("Player Data")] public TextMesh playerNameText;
    public Material playerMaterialClone;

    [Header("Client data")] [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    [SyncVar] public float speed = 5;
    [SyncVar] public List<Color> colors;


    private void Update()
    {
        if (!isLocalPlayer) return;
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CmdAddSpeed();
        }
    }

    public void HandleMovement()
    {
        float moveHor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVer = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(moveHor, moveVer, 0);
        transform.position = transform.position + movement;
    }

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
        FixPlayerName();
        FixPlayerColor();
    }

    private void FixPlayerColor()
    {
        RequestColors();
        Color currentCol = GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playerColor;
        int num = Random.Range(0, colors.Count - 1);
        if (currentCol == Color.white || !colors.Contains(currentCol))
        {
            currentCol = new Color(colors[num].r, colors[num].g, colors[num].b, 1);
            CmdRemoveColor(currentCol);
        }
        CmdSetupPlayerColor(currentCol);
    }
    private void FixPlayerName()
    {
        string playername = GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playername;
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        int i = 0;
        foreach (GameObject obj in gameObjects)
        {
            Player player = obj.GetComponent<Player>();
            if (!player.playerName.StartsWith(playername)) continue;
            i++;
        }
        if (i > 0) playername = playername + i;
        CmdSetupPlayerName(playername);
    }

    [Command]
    public void CmdSetupPlayerName(string newPlayerName)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        playerName = newPlayerName;
    }
    
    [Command]
    public void CmdSetupPlayerColor(Color color)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        playerColor = color;
    }

    [Command][Client]
    public void CmdUpdateAllInfo()
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        speed = server.GetComponent<Server>().Speed;
        colors = server.GetComponent<Server>().GetColors();
    }
    
    [Command][Client]
    public void RequestColors()
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) { return; }
        colors = GameObject.Find("Server").GetComponent<Server>().GetColors();
    }
    
    
    [Command]
    public void CmdAddSpeed()
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) return;
        server.GetComponent<Server>().Speed++;
    }

    [Command]
    public void CmdChangeColor(Color oldColor, Color newColor)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) return;
        server.GetComponent<Server>().ColorListAddColor(oldColor);
        server.GetComponent<Server>().ColorListRemoveColor(newColor);
    }

    [Command]
    public void CmdAddColor(Color col)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) return;
        server.GetComponent<Server>().ColorListAddColor(col);
    }

    [Command]
    public void CmdRemoveColor(Color col)
    {
        GameObject server = GameObject.Find("Server");
        if (server == null) return;
        server.GetComponent<Server>().ColorListRemoveColor(col);
    }
}
