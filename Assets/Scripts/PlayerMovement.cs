using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (_playerData.canMove)
        {
            float moveHor = Input.GetAxis("Horizontal") * _playerData.speed * Time.deltaTime;
            float moveVer = Input.GetAxis("Vertical") * _playerData.speed * Time.deltaTime;
            Vector3 movement = new Vector3(moveHor, moveVer, 0);
            transform.position = transform.position + movement;
        }
    }
}
