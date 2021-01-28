using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    [SerializeField] private GameObject pc;
    [SerializeField] private GameObject customizationMenu;
    private GameObject _useButton;
    private GameObject _customizeButton;

    private void Awake()
    {
        foreach (Transform child in GameObject.Find("PlayerCanvas").transform)
        {
            child.gameObject.SetActive(true);
        }
        
        pc = GameObject.Find("Pc");
        customizationMenu = GameObject.Find("CustomizationMenu");
        customizationMenu.SetActive(false);

        _customizeButton = (GameObject.Find("PcButtonUI"));
        _useButton = (GameObject.Find("UseButton"));
        
        DisableAllUserinterfaceButtons();
        EnableUserinterfaceButton(_useButton);
    }

    private void Update()
    {
        HandlePC();
    }

    public void HandlePC()
    {
        if (pc != null)
        {
            double distance = Vector2.Distance(pc.transform.position, this.transform.position);
            if (distance < 2)
            {
                DisableAllUserinterfaceButtons();
                EnableUserinterfaceButton(_customizeButton);
                //show the right UI & if e is pressed open customization menu
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenCustimizationMenu();
                    //make sure you cant move.. 
                    this.GetComponent<PlayerData>().canMove = false;
                }
                return;
            }
            DisableAllUserinterfaceButtons();
            EnableUserinterfaceButton(_useButton);
            customizationMenu.SetActive(false);
        }
    }

    private void OpenCustimizationMenu()
    {
        customizationMenu.SetActive(true);
    }

    public void DisableAllUserinterfaceButtons()
    {
        _useButton.SetActive(false);
        _customizeButton.SetActive(false);
    }

    public void EnableUserinterfaceButton(GameObject button)
    {
        button.SetActive(true);
    }
    
}
