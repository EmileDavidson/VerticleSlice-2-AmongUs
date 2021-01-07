using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Client : NetworkBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private GameObject hat;
    [SerializeField] private GameObject outFit;
    [SerializeField] private GameObject child;

    //getters & setters
    public Color Color
    {
        get => color;
        set
        {
            color = value;
            ChangePlayer();
        }
    }

    public GameObject Hat
    {
        get => hat;
        set
        {
            hat = value;
            ChangePlayer();
        }
    }

    public GameObject OutFit
    {
        get => outFit;
        set
        {
            outFit = value;
            ChangePlayer();
        }
    }

    public GameObject Child
    {
        get => child;
        set
        {
            child = value;
            ChangePlayer();
        }    
    }

    public void ChangePlayer()
    {
        // gameObject.GetComponent<SpriteRenderer>().color = color;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
