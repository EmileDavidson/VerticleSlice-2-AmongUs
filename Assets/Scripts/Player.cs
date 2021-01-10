using System;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;
public class Player : NetworkBehaviour
{
    [Header("Client Data")]
    private float speed = 5;
    public TextMesh playerNameText;
    public Material playerMaterialClone;
    
    [Header("Player data")]
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;
    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;

    private void Update()
    {
        if (!isLocalPlayer) return;
        HandleMovement();
    }

    public void HandleMovement()
    {
        float moveHor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveVer = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 movement = new Vector3(moveHor , moveVer, 0);
        transform.position = transform.position + movement;
    }
    
    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = _New;
    }
    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
        playerMaterialClone = new Material(GetComponent<Renderer>().material);
        playerMaterialClone.color = _New;
        GetComponent<Renderer>().material = playerMaterialClone;
    }
    public override void OnStartLocalPlayer()
    {
        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
        CmdSetupPlayer(name, color);
    }

    [Command]
    public void CmdSetupPlayer(string _name, Color _col)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
    }

}
