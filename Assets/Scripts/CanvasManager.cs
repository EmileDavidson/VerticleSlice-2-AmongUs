using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject inputField1;
    public void DeActive(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    public void Active(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void StartLocalHost()
    {
        if (GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playername.Trim() == "" )
        {
            inputField1.GetComponent<Animator>().SetBool("NameIsEmpty", true);
            return;
        }
        networkManager.StartHost();
    }

    public void StartClient()
    {
        if (GameObject.Find("PlayerDataBeforeJoin").GetComponent<PlayerDataBeforeJoin>().playername.Trim() == "" )
        {
            inputField1.GetComponent<Animator>().SetBool("NameIsEmpty", true);
            return;
        }
        networkManager.StartClient();
    }
    
    public void CloseGame()
    {
        Application.Quit();
    }
}
