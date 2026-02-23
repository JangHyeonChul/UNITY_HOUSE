using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Vector2 playerPosition;
    private Animator animator;

    private string playerStatus = "down";

    void Start()
    {
        GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Translate(playerPosition * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        playerPosition = new Vector2(moveInput.x, moveInput.y);

        if (moveInput.x + moveInput.y > 0) {
            animator.SetFloat("Speed", moveInput.x + moveInput.y);
        } else
        {
            animator.SetFloat("Speed", 0);
        }


        if (moveInput.y > 0.01)
        {
            playerStatus = "down";
        }

        if (playerStatus.Equals("down")) {
            animator.SetBool("IsDown", true);
        }

        Console.WriteLine("테스트1");
    }
}
