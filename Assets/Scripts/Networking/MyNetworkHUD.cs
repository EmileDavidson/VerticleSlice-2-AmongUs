using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkHUD : NetworkManagerHUD
{
    public class NetworkManagerHUD : MonoBehaviour
    {
        NetworkManager manager;

        void Awake()
        {
            manager = GetComponent<NetworkManager>();
        }

        public void StartServerAndClient()
        {
            manager.StartHost();
        }
    }
}
