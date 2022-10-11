using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Settings")]
    [SerializeField] float playerForwardSpeed;
    [SerializeField] float playerSidewaysSpeed;

    static public bool canMove = true;
    
    private Vector2 moveInput;
    private float movementSpeed;
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        movementSpeed = playerForwardSpeed;
    }
    
    void Update()
    {
        playerRigidbody.velocity = Vector3.forward * movementSpeed;

        if (canMove)
        {
            if ((moveInput.x < 0 && transform.position.x > LevelBoundary.leftSide) 
                || (moveInput.x > 0 && transform.position.x < LevelBoundary.rightSide))
            {
                playerRigidbody.AddRelativeForce(moveInput.x * (playerSidewaysSpeed * 15), 0, 0);
            }
        }
    }

    void OnSidewaysMovement(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void OnSlowDown(InputValue actionButton)
    {
        if (actionButton.isPressed){
            movementSpeed = playerForwardSpeed / 2;
        } else {
            movementSpeed = playerForwardSpeed;
        }
    }
}
