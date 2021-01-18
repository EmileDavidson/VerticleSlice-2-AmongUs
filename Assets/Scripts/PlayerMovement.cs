using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private PlayerData _playerData;
    [SerializeField] private GameObject playerBody;
    private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        _playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
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
            rb.transform.position = transform.position + movement;

            if (moveHor > 0) playerBody.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (moveHor < 0) playerBody.transform.rotation = new Quaternion(0, 180, 0, 0);
            
            if (moveHor != 0 || moveVer != 0) { animator.SetBool("Iswalking", true); }
            else { animator.SetBool("Iswalking", false); }
        }
    }
}
