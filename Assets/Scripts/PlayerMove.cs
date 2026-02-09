using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{    
    private Vector2 playerPosition;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(playerPosition * Time.deltaTime);
    }

    void OnMove(InputValue value) {

        Vector2 moveInput = value.Get<Vector2>();
        playerPosition = new Vector2(moveInput.x, moveInput.y);
    }
}
