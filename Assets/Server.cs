using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Server : NetworkBehaviour
{
    [Header("PlayerData (has to be synced with players)")]
    [SyncVar(hook = nameof(SpeedChanged))][SerializeField] private float speed = 5;
    [SyncVar][SerializeField] private List<Color> colorList;
    [SyncVar][SerializeField] private List<Color> usableColorList;
    [SyncVar][SerializeField] public int playersInGame;

    public void Awake()
    {
        usableColorList = colorList;
    }

    public void SpeedChanged(float oldVal, float newVal)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerData>().speed = newVal;
        }
    }
    
    [Server]
    public void ColorsChanged(List<Color> newVal)
    {
        print("TESTING 1.2.3.4.5.6");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerData>().colors = newVal;
            print("TESTING 1.2.3");
        }
    }

    [Server]
    public void ColorListRemoveColor(Color color)
    {
        print("<COLOR=red>" + colorList.Count + "</COLOR>");
        colorList.Remove(color);
        print("<COLOR=red>" + colorList.Count + "</COLOR>");
        ColorsChanged(colorList);
    }
    
    [Server]
    public void ColorListAddColor(Color col)
    {
        colorList.Add(col);
        ColorsChanged(colorList);
    }

    [Server]
    public List<Color> GetColors()
    {
        return colorList;
    }
    
    [Server]
    public List<Color> GetUsableColors()
    {
        return colorList;
    }

    
    #region getters en setters
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    #endregion


}
