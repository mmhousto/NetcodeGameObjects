using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float rotateSpeed = 50.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this);
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        HandleGravityJump();

        MovePlayer();

        RotatePlayer();

    }

    private void RotatePlayer()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed);
    }

    private void MovePlayer()
    {
        Vector3 move = transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    private void HandleGravityJump()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}