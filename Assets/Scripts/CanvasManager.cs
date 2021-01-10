using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    public void DeActive(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    public void Active(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void startLocalHost()
    {
        networkManager.StartHost();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
