using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = System.Random;

public class Server : NetworkBehaviour
{
    [Header("PlayerData (has to be synced with players)")]
    [SyncVar(hook = nameof(SpeedChanged))][SerializeField] private float speed = 5;
    [SerializeField] private List<Color> colorList;

    [Header("ServerData (can be requested by players)")]
    [SerializeField] private int imposters = 1;
    [SerializeField] private string map = "mapName";
    [SerializeField] private bool confirmEjects = true;
    [SerializeField] private int emergencyMeetings = 1;
    [SerializeField] private int emergencyCooldown = 1;
    [SerializeField] private int discussionTime = 1;
    [SerializeField] private int votingTime = 1;
    [SerializeField] private bool anonymousVote = false;
    [SerializeField] private float crewMateVision = 1;
    [SerializeField] private float imposterVision = 1;
    [SerializeField] private float killCooldown = 1;
    [SerializeField] private string killDistance = "medium";
    [SerializeField] private bool visualTasks = false;
    [SerializeField] private string taskBarUpdate = "meeting";
    [SerializeField] private int commonTasks = 1;
    [SerializeField] private int longTasks = 1;
    [SerializeField] private int shortTasks = 1;


    public void SpeedChanged(float oldVal, float newVal)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().speed = newVal;
        }
    }
    
    [Server]
    public void ColorsChanged(List<Color> newVal)
    {
        print("TESTING 1.2.3.4.5.6");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().colors = newVal;
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

    
    #region getters en setters
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public int Imposters
    {
        get => imposters;
        set => imposters = value;
    }
    public string Map
    {
        get => map;
        set => map = value;
    }
    public bool ConfirmEjects
    {
        get => confirmEjects;
        set => confirmEjects = value;
    }
    public int EmergencyMeetings
    {
        get => emergencyMeetings;
        set => emergencyMeetings = value;
    }
    public int EmergencyCooldown
    {
        get => emergencyCooldown;
        set => emergencyCooldown = value;
    }
    public int DiscussionTime
    {
        get => discussionTime;
        set => discussionTime = value;
    }
    public int VotingTime
    {
        get => votingTime;
        set => votingTime = value;
    }
    public bool AnonymousVote
    {
        get => anonymousVote;
        set => anonymousVote = value;
    }
    public float CrewMateVision
    {
        get => crewMateVision;
        set => crewMateVision = value;
    }
    public float ImposterVision
    {
        get => imposterVision;
        set => imposterVision = value;
    }
    public float KillCooldown
    {
        get => killCooldown;
        set => killCooldown = value;
    }
    public string KillDistance
    {
        get => killDistance;
        set => killDistance = value;
    }
    public bool VisualTasks
    {
        get => visualTasks;
        set => visualTasks = value;
    }
    public string TaskBarUpdate1
    {
        get => taskBarUpdate;
        set => taskBarUpdate = value;
    }
    public int CommonTasks
    {
        get => commonTasks;
        set => commonTasks = value;
    }
    public int ShortTasks
    {
        get => shortTasks;
        set => shortTasks = value;
    }
    public int LongTasks
    {
        get => longTasks;
        set => longTasks = value;
    }
    #endregion


}
