
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkManagerAmongUs : NetworkManager
{
    public List<Color> colors = new List<Color>();
    public List<GameObject> players = new List<GameObject>();

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
        
        NetworkServer.AddPlayerForConnection(conn, player);
        
        player.GetComponent<Client>().Color = GetStartingColor();
    }

    public Color GetStartingColor()
    {
        int num = Random.Range(0, colors.Count - 1);
        colors.RemoveAt(num);
        return colors[num];
    }
    public void ChangeColor(GameObject player, Color color)
    {
        if (colors.Contains(color))
        {
            colors.Remove(color);
            colors.Add(player.GetComponent<Client>().Color);
            player.GetComponent<Client>().Color = color;
            return;
        }
        print("Color not found is it already used?");
    }
}
