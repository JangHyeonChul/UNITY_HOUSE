using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    
    private Vector3 playerPosition;

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
        playerPosition = new Vector3(moveInput.x, 0, moveInput.y);
    }
}
