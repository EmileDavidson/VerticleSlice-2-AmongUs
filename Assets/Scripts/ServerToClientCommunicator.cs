using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ServerToClientCommunicator : NetworkBehaviour
{
    [TargetRpc]
    public void ColorListUpdate(List<Color> colorList)
    {
        GetComponent<PlayerData>().colors = colorList;
    }
}
